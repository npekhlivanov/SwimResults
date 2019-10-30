namespace Constants
{
    using System.ComponentModel.DataAnnotations;

    public static class Enums
    {
        public enum StrokeType
        {
            [Display(Name="Free")]
            Freestyle = 0,

            [Display(Name="Back")]
            Backstroke = 1,

            [Display(Name="Breast")]
            Breaststroke = 2,

            //[DisplayName("Fly")]
            Butterfly = 3,

            //[DisplayName("Drill")]
            Drill = 4,

            //[DisplayName("Kick")]
            Kick = 5,

            //[DisplayName("Not selected")]
            Unknown = 6
        }
    }
}
