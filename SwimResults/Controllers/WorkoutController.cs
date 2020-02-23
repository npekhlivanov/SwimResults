namespace SwimResults.Controllers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using DataImport;
    using DataModels;
    using DataTemplates.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using NP.Helpers.Extensions;
    using SwimResults.Models;
    using SwimResults.Tools;

    public class DTO
    {
        public string Value { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly IRepository<Workout> _workoutRepository;
        private readonly IConfiguration _configuration;

        public WorkoutController(IRepository<Workout> workoutRepository, IConfiguration configuration)
        {
            _workoutRepository = workoutRepository ?? throw new ArgumentNullException(nameof(workoutRepository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        // POST: api/Workout
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] int workoutId) //string value
        {
            // With .Net Core 3.0 string parameters must be passed as quoted, so the string parameter is changed to integer (since contentType is "application/json")
            // Otherwise JavaScript must use JSON.stringify() to pass the parameters (also when the parameter is an object)
            // In case of mismatched types the middleware returns HTTP result 415 to the caller
            if (workoutId <= 0)
            //if (!int.TryParse(value, out int workoutId))
            {
                return CreateErrorResponse($"Invalid workoutId ({workoutId})!");
            }

            FillWorkoutDetailsResult result;
            try
            {
                var workout = await _workoutRepository.GetById(workoutId, w => w.Intervals);
                if (workout == null)
                {
                    return CreateErrorResponse($"Workout with Id={workoutId} not found!");
                }

                if (workout.Intervals?.Count > 0)
                {
                    return CreateErrorResponse($"The details for workout with Id={workoutId} have already been loaded!");
                }

                var loadWorkoutResult = await LoadWorkoutDetails(workout);
                if (!loadWorkoutResult.Success)
                {
                    return CreateErrorResponse(loadWorkoutResult.Message);
                }

                await _workoutRepository.Update(workout);

                var workoutData = new WorkoutDetailsData
                {
                    WorkoutId = workoutId,
                    Distance = workout.Distance.ToStringInvariant(),
                    Duration = DisplayValuesFormatter.FormatDuration(workout.Duration, false),
                    Name = workout.Name,
                    Pace = DisplayValuesFormatter.FormatDuration(workout.Pace, false),
                    StartDate = workout.WorkoutDate.ToShortDateString()
                };
                result = new FillWorkoutDetailsResult
                {
                    Success = true,
                    WorkoutData = workoutData,
                    Message = loadWorkoutResult.Message
                };

                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return CreateErrorResponse($"An error occurred: {ex.Message}");
            }
        }

        private class LoadWorkoutDetailsResult
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }

        private async Task<LoadWorkoutDetailsResult> LoadWorkoutDetails(Workout workout)
        {
            var result = new LoadWorkoutDetailsResult();

            var workoutDetailsFolder = _configuration["WorkoutDetailsFolder"];
            workoutDetailsFolder = Environment.ExpandEnvironmentVariables(workoutDetailsFolder);
            var workoutDetailsFile = Path.Combine(workoutDetailsFolder, $"{workout.Id}.xml");
            if (System.IO.File.Exists(workoutDetailsFile))
            {
                result.Success = WorkoutDetailParser.LoadWorkoutData(workoutDetailsFile, workout);
                result.Message = result.Success ? $"Loaded details for workout {workout.Id} from file" : $"Details were not loaded for workout {workout.Id}";
            }
            else
            {
                using (var detailsStream = await WorkoutDetailsRetriever.DownloadWorkoutDetails(_configuration["OnlineWorkoutDetailsSource"], workout.Id))
                {
                    if (detailsStream == null)
                    {
                        result.Message = $"No details found for workout with Id={workout.Id}!";
                        result.Success = false;
                        return result;
                    }

                    detailsStream.Position = 0;
                    WorkoutDetailParser.LoadWorkoutData(detailsStream, workout);

                    using var outputFile = new FileStream(workoutDetailsFile, FileMode.Create);
                    detailsStream.Position = 0;
                    await detailsStream.CopyToAsync(outputFile);
                }

                result.Success = true;
                result.Message = $"Loaded details for workout {workout.Id} from online source";
            }

            return result;
        }

        private static JsonResult CreateErrorResponse(string errorMessage)
        {
            var result = new FillWorkoutDetailsResult
            {
                Success = false,
                Message = errorMessage
            };

            return new JsonResult(result);
        }

        /// <summary>
        /// Do not make the method static!<para/>
        /// This will result in "405 HTTP Method Not Supported / Method not allowed" when calling it.<para/>
        /// Besides this, @Url.Action("GetWorkoutName", "Workout") returns empty string
        /// </summary>
        /// <param name="value">The date of the workout (yyyy-mm-dd)</param>
        /// <returns>Workout name, containing the date</returns>
        // GET: api/Workout
        [HttpGet]
        public IActionResult GetWorkoutName(string value)
        {
            string result;
            if (!string.IsNullOrEmpty(value) && DateTime.TryParse(value, out DateTime date))
            {
                var dateAndTime = new DateTime(date.Year, date.Month, date.Day, DateTime.Now.Hour, 0, 0);
                result = ValuesHelper.ComposeWorkoutName(dateAndTime);
            }
            else
            {
                result = string.Empty;
            }

            return new JsonResult(result);
        }

        // GET: api/Workout
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Workout/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // PUT: api/Workout/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
