namespace Constants
{
    using System.ComponentModel;

    public static class Enums
    {
        public enum StrokeType
        {
            [DisplayName("Free")]
            Freestyle = 0,

            [DisplayName("Back")]
            Backstroke = 1,

            [DisplayName("Breast")]
            Breaststroke = 2,

            [DisplayName("Fly")]
            Butterfly = 3,

            [DisplayName("Drill")]
            Drill = 4,

            [DisplayName("Kick")]
            Kick = 5,

            [DisplayName("Not selected")]
            Unknown = 6
        }
    }
}
