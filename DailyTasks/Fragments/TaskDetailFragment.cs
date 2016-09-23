using System;
using System.Linq;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Views.Animations;

using Fragment = Android.Support.V4.App.Fragment;
using FloatingActionButton = Clans.Fab.FloatingActionButton;
using System.Collections.Generic;
using Clans.Fab;
using DailyTasks.Core;
using Android.Views.InputMethods;

namespace DailyTasks
{
	public class TaskDetailFragment : Fragment
	{
		Task task = new Task();
		Button cancelDeleteButton;
		EditText notesTextEdit;
		EditText nameTextEdit;
		Button saveButton;
		CheckBox doneCheckbox;
		private int TaskId;

		public TaskDetailFragment() { }

		public TaskDetailFragment(int taskId)
		{
			TaskId = taskId;
		}

		public override void OnViewCreated(View view, Bundle savedInstanceState)
		{
			base.OnViewCreated(view, savedInstanceState);

			nameTextEdit = view.FindViewById<EditText>(Resource.Id.NameText);
			notesTextEdit = view.FindViewById<EditText>(Resource.Id.NotesText);
			saveButton = view.FindViewById<Button>(Resource.Id.SaveButton);
			doneCheckbox = view.FindViewById<CheckBox>(Resource.Id.chkDone);
			cancelDeleteButton = view.FindViewById<Button>(Resource.Id.CancelDeleteButton);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			return inflater.Inflate(Resource.Layout.TaskDetails, container, false);
		}

		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);

			if (TaskId > 0)
			{
				task = TaskManager.GetTask(TaskId);

				nameTextEdit.Text = task.Name;
				notesTextEdit.Text = task.Notes;
				doneCheckbox.Checked = task.Done;
			} 

			// set the cancel delete based on whether or not it's an existing task
			cancelDeleteButton.Text = (task.ID == 0 ? "Cancel" : "Delete");

			// button clicks 
			cancelDeleteButton.Click += (sender, e) => { 
				CancelDelete();
				HideKeyboard();
			};
			saveButton.Click += (sender, e) => {
				Save();
				HideKeyboard();			
			};
		}

		void Save()
		{
			task.Name = nameTextEdit.Text;
			task.Notes = notesTextEdit.Text;
			task.Done = doneCheckbox.Checked;

			if (TaskId == 0)
				TaskManager.SaveTask(task);
			else
				TaskManager.UpdateTask(task);

			ReturnHome();
		}

		void CancelDelete()
		{
			if (task.ID != 0)
			{
				TaskManager.DeleteTask(task.ID);
			}

			ReturnHome();
		}

		void HideKeyboard()
		{
			var main = (MainActivity)this.Activity;

			main.imm.HideSoftInputFromWindow(nameTextEdit.WindowToken, 0);
			main.imm.HideSoftInputFromWindow(notesTextEdit.WindowToken, 0);
		}

		void ReturnHome()
		{
			var myActivity = (MainActivity)this.Activity;
			myActivity.ChangeFragment(Resource.Id.tasks);
		}

	}
}
