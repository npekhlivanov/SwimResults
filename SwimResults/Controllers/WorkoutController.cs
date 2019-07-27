namespace SwimResults.Controllers
{
    using DataAccess.Data;
    using DataAccess.Models;
    using DataImport;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using SwimResults.Models;
    using SwimResults.Tools;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly WorkoutRepository _workoutRepository;
        private readonly IConfiguration _configuration;

        public WorkoutController(WorkoutRepository workoutRepository, IConfiguration configuration)
        {
            _workoutRepository = workoutRepository;
            _configuration = configuration;
        }

        // POST: api/Workout
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            FillWorkoutDetailsResult result;
            if (!int.TryParse(value, out int workoutId))
            {
                return CreateErrorResponse($"Invalid workoutId ({value})!");
            }

            try
            {
                var workout = await _workoutRepository.Get(workoutId, w => w.Intervals);
                if (workout == null)
                {
                    return CreateErrorResponse($"Workout with Id={value} not found!");
                }

                if (workout.Intervals?.Count > 0)
                {
                    return CreateErrorResponse($"The details for workout with Id={value} have already been loaded!");
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
                    Distance = workout.Distance.ToString(),
                    Duration = DisplayValuesFormatter.FormatDuration(workout.Duration, false),
                    Name = workout.Name,
                    Pace = DisplayValuesFormatter.FormatDuration(workout.Pace, false),
                    StartDate = workout.Date.ToShortDateString()
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
            var workoutDetailsFile = Path.Combine(workoutDetailsFolder, $"{workout.Id}.xml");
            if (System.IO.File.Exists(workoutDetailsFile))
            {
                WorkoutDetailParser.LoadWorkoutData(workoutDetailsFile, workout);
                result.Message = $"Loaded details for workout {workout.Id} from file";
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

                    using (var outputFile = new FileStream(workoutDetailsFile, FileMode.Create))
                    {
                        detailsStream.Position = 0;
                        await detailsStream.CopyToAsync(outputFile);
                    }
                }

                result.Message = $"Loaded details for workout {workout.Id} from online source";
            }

            result.Success = true;
            return result;
        }

        private JsonResult CreateErrorResponse(string errorMessage)
        {
            var result = new FillWorkoutDetailsResult
            {
                Success = false,
                Message = errorMessage
            };

            return new JsonResult(result);
        }

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
