namespace Website.Application.Common.Interfaces
{
    public interface ITaskService
    {
        HashSet<string> CompletedTasks { get; set; }
    }
}
