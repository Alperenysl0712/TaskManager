namespace TaskManager.Models
{
    public enum TaskPriority
    {
        Low,
        Normal,
        High
    }

    public enum TaskStatus
    {
        Pending,
        InProgress,
        Completed,
        Cancelled
    }

    public enum TaskPeriod
    {
        Daily,
        Weekly,
        Monthly
    }

}
