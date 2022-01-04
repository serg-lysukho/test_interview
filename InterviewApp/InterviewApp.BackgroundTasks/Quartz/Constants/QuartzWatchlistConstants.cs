using InterviewApp.BackgroundTasks.Quartz.Jobs;

namespace InterviewApp.BackgroundTasks.Quartz.Constants
{
    public static class QuartzWatchlistConstants
    {
        public const string SchedulerName = "Interview App Watchlist Scheduler";
        public const string JobName = nameof(WatchlistEmailNotificationJob);

        public const string CronTriggerExpression = "0 30 19 ? * SUN *";
    }
}
