using SQLite;
using ToDo.Models;

namespace TodoSQLite.Data;

public class TasksService
{
    private SQLiteAsyncConnection db;
    public TasksService()
    {
    }
     private async Task Init()
    {
        if (db is not null)
            return;

        db = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        await db.CreateTableAsync<TaskModel>();
    }

    public async Task<List<TaskModel>> GetTasksAsync()
    {
        await Init();
        return await db.Table<TaskModel>().ToListAsync();
    }

    public async Task<int> SaveTaskAsync(TaskModel task)
    {
        await Init();
        if (task.ID != 0)
        {
            return await db.UpdateAsync(task);
        }
        else
        {
            return await db.InsertAsync(task);
        }
    }

    public async Task<int> DeleteTaskAsync(TaskModel task)
    {
        await Init();
        return await db.DeleteAsync(task);
    }
}