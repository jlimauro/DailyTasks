
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

namespace DailyTasks
{
	public class HomeFragment : Fragment
	{
		private FloatingActionButton fab;
		private FloatingActionMenu menuRed; 
		private int previousVisibleItem;
		private ListView listView;
		private readonly bool hideFab;

		public override void OnViewCreated(View view, Bundle savedInstanceState)
		{
			base.OnViewCreated(view, savedInstanceState);
			this.listView = view.FindViewById<ListView>(Resource.Id.taskList);
			this.fab = view.FindViewById<FloatingActionButton>(Resource.Id.addTask);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			return inflater.Inflate(Resource.Layout.home_fragment, container, false);
		}

		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);

			var tasks = new List<string>() { "Take Medicine", "Go to work", "Call Boyfriend" };

			// Here you would edit adaptor to add button and events.
			this.listView.Adapter = new ArrayAdapter(this.Activity, Android.Resource.Layout.SimpleListItem1,
				Android.Resource.Id.Text1, tasks);
		}
	}
}
