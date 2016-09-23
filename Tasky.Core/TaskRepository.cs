using System;
using System.Collections.Generic;
using SQLite;

namespace DailyTasks.Core
{
	class TaskRepository
	{
		static readonly string dbPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

		public static Task GetTask(int id)
		{
			Task taskData = null;
			try
			{
				using (SQLiteConnection dbConn = new SQLiteConnection(System.IO.Path.Combine(dbPath, "TaskListData.db")))
				{
					taskData = dbConn.Get<Task>(id);
				}
			}
			catch
			{
				Console.WriteLine("Unable to get task");
			}

			return taskData;
		}

		public static IEnumerable<Task> GetTasks()
		{
			try
			{
				using (SQLiteConnection dbConn = new SQLiteConnection(System.IO.Path.Combine(dbPath, "TaskListData.db")))
				{
					var mItems = from tData in dbConn.Table<Task>() select tData;

					var tasks = new List<Task>();

					foreach (var item in mItems)
					{
						tasks.Add(new Task() { 						
							ID = item.ID,
							Name = item.Name,
							Notes = item.Notes,
							Done = item.Done						
						});
					}

					return tasks;
				}
			}
			catch 
			{
				Console.WriteLine("Unable to get tasks");
				return null;
			}
		}

		public static int UpdateTask(Task item)
		{
			int retVal = 0;

			using (SQLiteConnection dbConn = new SQLiteConnection(System.IO.Path.Combine(dbPath, "TaskListData.db")))
			{
				try
				{
					retVal = dbConn.Update(item);
					Console.WriteLine("Task Updated");
				}
				catch
				{
					Console.WriteLine("Error Updating Task");
					return -1;
				}
			}
			return retVal;
		}

		public static int SaveTask(Task item)
		{
			int retVal = 0;

			using (SQLiteConnection dbConn = new SQLiteConnection(System.IO.Path.Combine(dbPath, "TaskListData.db")))
			{
				try
				{
					retVal = dbConn.Insert(new Task()
					{
						ID = item.ID,
						Name = item.Name,
						Notes = item.Notes,
						Done = item.Done
					});
					Console.WriteLine("Task Saved");
				}
				catch
				{
					Console.WriteLine("Error Saving Task");
					return -1;
				}
			}
			return retVal;
		}

		public static void CreateDatabase()
		{
			using (SQLiteConnection dbConn = new SQLiteConnection(System.IO.Path.Combine(dbPath, "TaskListData.db")))
			{
				// Create DB from view model
				dbConn.CreateTable<Task>();
			}
		}

		public static int DeleteTask(int id)
		{
			int retVal = 0;
			try
			{
				using (SQLiteConnection dbConn = new SQLiteConnection(System.IO.Path.Combine(dbPath, "TaskListData.db")))
				{
					Task item = dbConn.Get<Task>(id);
					retVal = dbConn.Delete(item);
				}
			}
			catch
			{
				Console.WriteLine("Unable to delete task");
			}
			return retVal;
		}
	}
}