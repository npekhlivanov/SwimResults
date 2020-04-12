using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SwimResults.Models.Api;
using SwimResults.Tools;

namespace SwimResults.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntervalsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> ModifyLengths([FromBody] ModifyIntervalLengthsRequest requestData)
        {
            if (requestData?.IntervalData == null)
            {
                return new JsonResult(new ApiResultBase { Success = false, Message = "No input data!" });
            }

            if (requestData.SelectedLengthNo <= 0)
            {
                return new JsonResult(new ApiResultBase { Success = false, Message = "Invalid Length No!" });
            }

            var inputInterval = requestData.IntervalData;
            if (inputInterval.Lengths?.Count == 0)
            {
                return new JsonResult(new ApiResultBase { Success = false, Message = "No Lengths!" });
            }

            var interval = DisplayDataHelper.CreateInterval(inputInterval);

            // Perform modifications

            var resultInterval = DisplayDataHelper.CreateIntervalDisplayData(interval);
            resultInterval.IntervalTypeName = inputInterval.IntervalTypeName;

            var result = new ModifyIntervalLengthsResponse
            {
                Success = true,
                Result = resultInterval
            };
            return new JsonResult(result);
        }

    }
}