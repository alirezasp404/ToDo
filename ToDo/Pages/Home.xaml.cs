


using System.Collections.ObjectModel;
using ToDo.Models;
using TodoSQLite.Data;


namespace ToDo.Pages;

public partial class Home : ContentPage
{
    private readonly TasksService taskService;

    public ObservableCollection<TaskModel> Tasks { get; set; }=new ObservableCollection<TaskModel>();
    public string? Description { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Now;
    public TimeSpan StartTime { get; set; } = TimeSpan.FromHours(1);
    public bool ValidInput => Description != null ;
    public Home(TasksService taskService)
    {
        BindingContext = this;
        InitializeComponent();
        this.taskService = taskService;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        await RefreshList();
    }
    private async Task RefreshList()
    {
        var tasks = await taskService.GetTasksAsync();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Tasks.Clear();
            foreach (var task in tasks)
                Tasks.Add(task);

        });
    }
    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (!ValidInput)
                throw new Exception("Valid Input required");

            await taskService.SaveTaskAsync((new TaskModel()
            {
                Description = this.Description,
                StartDate = this.StartDate,
                StartTime = this.StartTime,
                

            }));

            await DisplayAlert("Add Task", "New Task added successfully", "OK");
            descriptionInput.Text = null;
           await RefreshList();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Add Task", $"Failed to add new task :{ex.Message}", "OK");
        }
    }
    private async void DoneButton_Clicked(object sender, EventArgs e)
    {

        try
        {
            var doneTask = (TaskModel)((ImageButton)sender).BindingContext;

            await taskService.DeleteTaskAsync(doneTask);
            await RefreshList();
        }
        catch (Exception ex)
        {
          
            await DisplayAlert("Done Task", $"Failed to delete Done Task :{ex.Message}", "OK");
        }
    }

    private async void EditButton_Clicked(object sender, EventArgs e)
    {
        var editTask = (TaskModel)((ImageButton)sender).BindingContext;

        await Shell.Current.GoToAsync(nameof(Edit), true, new Dictionary<string, object>
        {
            ["Task"] = editTask
        });

    }

}