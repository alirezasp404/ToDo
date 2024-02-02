
using SQLite;

namespace ToDo.Models
{

    public class TaskModel
    {
        [PrimaryKey, AutoIncrement]

        public int ID { get; set; }

        public string? Description { get; set; }

        public DateTime StartDateTime { get; set; }

        [Ignore]
        public TimeSpan StartTime
        {
            get => StartDateTime.TimeOfDay;
            set => StartDateTime = new DateTime(StartDateTime.Year, StartDateTime.Month, StartDateTime.Day, value.Hours, value.Minutes, value.Seconds);
        }
        [Ignore]
        public DateTime StartDate
        {
            get => StartDateTime;
            set => StartDateTime = new DateTime(value.Year, value.Month, value.Day, StartDateTime.Hour, StartDateTime.Minute, StartDateTime.Second);
        }
    }
}
