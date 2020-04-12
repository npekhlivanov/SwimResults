namespace SwimResults.Models.Api
{
    public class ModifyIntervalLengthsRequest
    {
        public IntervalDisplayData IntervalData { get; set; }

        public int Mode { get; set; }

        public int SelectedLengthNo { get; set; }
    }
}
