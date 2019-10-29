namespace DataImport.Models.JSON
{
    internal class FeedListItem
    {
        public int WorkoutId { get; set; }
        public string WorkoutTitle { get; set; }
        public long WorkoutDate { get; set; }

        public int FeedLogId { get; set; }
        public int EventType { get; set; }
        public long FeedDate { get; set; }
        public object ModifiedDate { get; set; }
        public string FeedDateFormatted { get; set; }
        public WorkoutData WorkoutData { get; set; }
        public object CheckinData { get; set; }
        public object BlastData { get; set; }
        public object LeaderboardData { get; set; }
        public bool IsUserLiked { get; set; }
        public int TotalLike { get; set; }
        public int TotalComment { get; set; }
        public User User { get; set; }
        public string SocialName { get; set; }
        public string UnitText { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRows { get; set; }
        public string WorkoutDateFormatted { get; set; }
        public object ListUserLike { get; set; }
        public object ListComment { get; set; }
        public string WorkoutTitleFormated { get; set; }
        public int TotalPages { get; set; }
    }
}
