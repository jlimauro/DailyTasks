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

		public int TaskId { get; set; } = 0;

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
			cancelDeleteButton.Click += (sender, e) => { CancelDelete(); };
			saveButton.Click += (sender, e) => { Save(); };
		}

		void Save()
		{
			task.Name = nameTextEdit.Text;
			task.Notes = notesTextEdit.Text;
			task.Done = doneCheckbox.Checked;
			TaskManager.SaveTask(task);
		}

		void CancelDelete()
		{
			if (task.ID != 0)
			{
				TaskManager.DeleteTask(task.ID);
			}
		}

	}
}
