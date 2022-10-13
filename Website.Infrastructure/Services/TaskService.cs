using Website.Application.Common.Interfaces;

namespace Website.Infrastructure.Services
{
    public class TaskService : ITaskService
    {
        public HashSet<string> CompletedTasks { get; set; } = new HashSet<string>();
    }
}