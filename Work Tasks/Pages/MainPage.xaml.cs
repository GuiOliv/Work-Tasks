using Android.Runtime;
using Microsoft.Maui.Controls.Xaml;
using Syncfusion.Maui.DataSource.Extensions;
using System.Collections.ObjectModel;
using Work_Tasks.Classes;
using Work_Tasks.Exceptions;

namespace Work_Tasks;

public partial class MainPage : ContentPage
{
    Classes.Task currentItemSelected { get; set; }
    Company company
    {
        get
        {
            return ((App)Application.Current).company;
        }
    }

    ObservableCollection<Classes.Task> tasks { get; set; }
    List<string> filteredDepartment { get; set; } = new List<string>();
    public MainPage()
	{
		InitializeComponent();
		tasks = company.tasks.ToObservableCollection();
		BindingContext = company;
		taskList.ItemsSource = tasks;
		statusTxt.ItemsSource = Enum.GetValues(typeof(Status));
		statusFilter.ItemsSource = Enum.GetValues(typeof(Status));
    }

    private async void addBtn_Clicked(object sender, EventArgs e)
    {
        await addBtn.ScaleTo(0.75, 100);
        await addBtn.ScaleTo(1, 100);
        
		try
		{
            if (company.tasks.Exists(w => w.name == nameTxt.Text))
            {
                throw new ExistingTaskException("You can´t have 2 tasks with the same name");
            }
			if (statusTxt.SelectedItem == null)
			{
				throw new NullStatusException("Select a status for the task");
			}

            company.BuildTask(nameTxt.Text, CheckBoxInput(), statusTxt.SelectedItem.ToString(), deadlineTxt.Text, authorTxt.Text, descTxt.Text);
        }
        catch (ExistingTaskException etEx)
        {
            await DisplayAlert("Error", etEx.Message, "OK");
            return;
        }
        catch (System.NullReferenceException st)
        {
            await DisplayAlert("Error",st.Message, "OK");
            return;
        }
        catch (FormatException)
		{
			await DisplayAlert("Error", "Write a valid Date", "OK");
            return;
        }
        catch (InvalidOperationException)
		{
			await DisplayAlert("Error", "There is no employee called " + authorTxt.Text, "OK");
            return;
		}

		nameTxt.Text = "";
		statusTxt.SelectedItem = statusTxt.Placeholder;
		deadlineTxt.Text = "";
		authorTxt.Text = "";
		descTxt.Text = "";

		LoadCollection();
    }

    private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        Filter();
    }

	private void LoadCollection()
	{
		tasks.Clear();
		foreach (var task in company.tasks)
		{
			tasks.Add(task);
		}
	}

    private void statusFilter_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
		Filter();
    }

	private void Filter()
	{
        LoadCollection();
        var searchTerm = searchBar.Text;

        if (String.IsNullOrWhiteSpace(searchTerm))
        {
            searchTerm = string.Empty;
        }

        searchTerm = searchTerm.ToLowerInvariant();

        
        var filteredItems = company.tasks.Where(w => w.name.ToLowerInvariant().Contains(searchTerm) && (statusFilter.SelectedItem != null ? w.status == (Status)Enum.Parse(typeof(Status), statusFilter.SelectedItem.ToString()) : true) && (filteredDepartment.Count == 0 ? true: filteredDepartment.Intersect(w.departments).Any())).ToList();
        
        foreach (var task in company.tasks)
        {
            if (!filteredItems.Contains(task))
            {
                tasks.Remove(task);
            }
            else if (!tasks.Contains(task))
            {
                tasks.Add(task);
            }
        }
    }

    private void hrCb_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        CheckBoxChecked("Human Resources", e);
    }

    private void rdCb_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        CheckBoxChecked("Research and Development", e);
    }

    private void supCb_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        CheckBoxChecked("Support", e);
    }

    private void mCb_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        CheckBoxChecked("Marketing", e);
    }

    private void sCb_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        CheckBoxChecked("Sales", e);
    }

    private void CheckBoxChecked(string depart, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            filteredDepartment.Add(depart);
        }
        else if (!e.Value)
        {
            filteredDepartment.Remove(depart);
        }
        Filter();
    }

    private List<string> CheckBoxInput()
    {
        List<string> input = new List<string>();

        if (hrInput.IsChecked)
        {
            input.Add("Human Resources");
        }
        if (rdInput.IsChecked)
        {
            input.Add("Research and Development");
        }
        if (supInput.IsChecked)
        {
            input.Add("Support");
        }
        if (mInput.IsChecked)
        {
            input.Add("Marketing");
        }
        if (sInput.IsChecked)
        {
            input.Add("Sales");
        }
        
        return input;
    }

    private async void removeBtn_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (company.tasks.First(w => w.name == removeTxt.Text).status != Status.Open)
            {
                throw new OngoingTaskException("This task can't be deleted since it's ongoing");
            }
            company.tasks.Remove(company.tasks.First(w => w.name == removeTxt.Text));
            Filter();
            try
            {
                DB.DeleteTask(removeTxt.Text);
            }
            catch (Exception generic)
            {
                await DisplayAlert("Error", generic.Message, "OK");
            }
            removeTxt.Text = "";
        }
        catch (OngoingTaskException OnTaskEx)
        {
            await DisplayAlert("Task is Ongoing", OnTaskEx.Message, "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
            statusFilter.SelectedItem = null;
        }
    }
}

