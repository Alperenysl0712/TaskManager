using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.Repositories;

namespace TaskManager.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly IRepository<TaskItem> _taskRepository;

        public TaskController(IRepository<TaskItem> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskModel task)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (task == null)
            {
                return BadRequest(new { message = "Task boş olamaz" });
            }

            TaskItem taskItem = new()
            {
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Priority = task.Priority,
                Status = task.Status,
                Period = task.Period,
                UserId = userId
            };

            await _taskRepository.Ekle(taskItem);
            return Ok(new { message = "Görev başarıyla oluşturuldu." });
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllTasks([FromQuery] TaskPeriod? period)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var role = User.FindFirstValue(ClaimTypes.Role);

            var tasks = await _taskRepository.GetAllAsync();

            var filtered = tasks.Where(t =>
                (period == null || t.Period == period) &&
                (role == "Admin" || t.UserId == userId));

            return Ok(filtered.OrderByDescending(t => t.CreatedAt));
        }

    }
}
