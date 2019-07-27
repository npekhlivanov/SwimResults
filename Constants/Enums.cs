namespace Constants
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class Enums
    {
        public enum StrokeType
        {
            [Display(Name="FreeStyle", Description="Free")]
            Freestyle = 0,
            [Description("Back")]
            Backstroke = 1,
            [Description("Breast")]
            Breaststroke = 2,
            [Description("Fly")]
            Butterfly = 3,
            [Description("Drill")]
            Drill = 4,
            [Description("Kick")]
            Kick = 5,
            [Description("Not selected")]
            Unknown = 6
        }
    }
}
