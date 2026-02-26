using System.Linq;

namespace Mission08_0106.Models;

public class EFTaskRepository : ITaskRepository
{
    private TaskDbContext _context;

    public EFTaskRepository(TaskDbContext context)
    {
        _context = context;
    }

    public IQueryable<TaskItem> Tasks => _context.Tasks;

    public IQueryable<Category> Categories => _context.Categories;

    public void AddTask(TaskItem task)
    {
        _context.Tasks.Add(task);
    }

    public void UpdateTask(TaskItem task)
    {
        _context.Tasks.Update(task);
    }

    public void DeleteTask(TaskItem task)
    {
        _context.Tasks.Remove(task);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}
