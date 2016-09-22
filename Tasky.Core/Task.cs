using System;
using SQLite;

namespace DailyTasks.Core {
	/// <summary>
	/// Task business object
	/// </summary>
	public class Task {

		[PrimaryKey, AutoIncrement]
        public int ID { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }
		public bool Done { get; set; }
	}
}