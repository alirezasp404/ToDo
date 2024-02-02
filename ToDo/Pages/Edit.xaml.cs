

using ToDo.Models;
using TodoSQLite.Data;

namespace ToDo.Pages;
[QueryProperty("Task", "Task")]
public partial class Edit : ContentPage
{

    private TaskModel task;
    public TaskModel Task
    {
        get=>task;
        set{ task = value;
            BindingContext = task;  }
    }

    private readonly TasksService tasksService;
    private bool validInput => task.Description != null;


    public Edit(TasksService tasksService)
	{

        InitializeComponent();
        this.tasksService = tasksService;
        
    }

    private async  void SubmitButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (!validInput)
                throw new Exception("Valid Input required");
            await tasksService.SaveTaskAsync(task);
            await DisplayAlert("Edit Task", "The existing Task edited successfully", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Edit Task", $"Failed to edit existing Task :{ex.Message}", "OK");
        }
    }
}