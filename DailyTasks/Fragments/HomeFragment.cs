
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
	public class HomeFragment : Fragment
	{
		private FloatingActionButton fab;
		private ListView listView;
		Adapters.TaskListAdapter taskList;
		IList<Task> tasks;

		public override void OnViewCreated(View view, Bundle savedInstanceState)
		{
			base.OnViewCreated(view, savedInstanceState);
			this.listView = view.FindViewById<ListView>(Resource.Id.taskList);
			this.fab = view.FindViewById<FloatingActionButton>(Resource.Id.addTask);

			TaskManager.CreateDatabase();
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			return inflater.Inflate(Resource.Layout.home_fragment, container, false);
		}

		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);

			tasks = TaskManager.GetTasks();

			// create our adapter

			try
			{
				taskList = new Adapters.TaskListAdapter(this.Activity, tasks);
				//Hook up our adapter to our ListView
				listView.Adapter = taskList;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + "Error: " + ex.InnerException);
			}

			if (listView != null)
			{
				listView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
				{
					var taskDetails = new TaskDetailFragment();
					taskDetails.TaskId = tasks[e.Position].ID;
				};
			}

			fab.Click += (sender, e) => { 
			
				var taskDetails = new TaskDetailFragment();			
			};
		}

		//public override void OnResume()
		//{
		//	base.OnResume();	


		//}
	}
}
