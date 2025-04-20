using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace TaskManager.Models
{
    public class TaskModel
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DueDate { get; set; }
        public TaskPriority Priority { get; set; } = TaskPriority.Normal;
        public TaskStatus Status { get; set; } = TaskStatus.Pending;
        public TaskPeriod Period { get; set; } = TaskPeriod.Daily;
    }
}
