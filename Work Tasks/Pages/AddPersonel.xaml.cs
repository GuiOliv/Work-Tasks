using Syncfusion.Maui.DataSource.Extensions;
using System.Collections.ObjectModel;
using Work_Tasks.Classes;

namespace Work_Tasks.Pages;

public partial class AddPersonel : ContentPage
{
	Company company
	{
		get
		{
			return ((App)Application.Current).company;
        }
	}
    ObservableCollection<Classes.Task> tasks { get; set; }

    public AddPersonel()
	{
		InitializeComponent();

        tasks = company.tasks.ToObservableCollection();

        taskList.ItemsSource = tasks;

		taskCombo.ItemsSource = company.tasks;

    }

    private async void addBtn_Clicked(object sender, EventArgs e)
    {
        await addBtn.ScaleTo(0.75, 100);
        await addBtn.ScaleTo(1, 100);

        if (taskCombo.SelectedItem != null)
		{
            ((Classes.Task)taskCombo.SelectedItem).AddEmployee((Employee)employeeCombo.SelectedItem);
        }
        LoadCollection();
    }

    private void searchName_TextChanged(object sender, TextChangedEventArgs e)
    {
        Filter();
    }

    private void Filter()
    {
        LoadCollection();
        var searchTerm = searchName.Text;

        if (String.IsNullOrWhiteSpace(searchTerm))
        {
            searchTerm = string.Empty;
        }

        List<Classes.Task> filteredTasks = new List<Classes.Task>();

        foreach (var task in company.tasks)
        {
            foreach (var employee in task.employees)
            {
                if (employee.fullName.Contains(searchTerm))
                {
                    filteredTasks.Add(task);
                }
            }
        }
        
        foreach (var task in company.tasks)
        {
            if (!filteredTasks.Contains(task))
            {
                tasks.Remove(task);
            }
            else if (!tasks.Contains(task))
            {
                tasks.Add(task);
            }
        }
    }
    private void LoadCollection()
    {
        tasks.Clear();
        foreach (var task in company.tasks)
        {
            Console.WriteLine(task.employeesString);
            tasks.Add(task);
        }
        taskList.ItemsSource = tasks;

        taskCombo.ItemsSource = company.tasks;

    }

    private void taskCombo_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        employeeCombo.ItemsSource = company.employees.Where(w => ((Classes.Task)taskCombo.SelectedItem).departments.Contains(w.department));
    }
}