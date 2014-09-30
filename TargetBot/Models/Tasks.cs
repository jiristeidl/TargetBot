using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TargetBot.Models
{
    public class Task
    {
        public DateTime? CreateDate { get; set; }

        public List<object> CustomFields { get; set; }

        public object Description { get; set; }

        public double Effort { get; set; }

        public double EffortCompleted { get; set; }

        public double EffortToDo { get; set; }

        public object EndDate { get; set; }

        public EntityState EntityState { get; set; }

        public EntityType EntityType { get; set; }

        public int Id { get; set; }

        public Iteration Iteration { get; set; }

        public object LastCommentDate { get; set; }

        public object LastCommentedUser { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string Name { get; set; }

        public double NumericPriority { get; set; }

        public Owner Owner { get; set; }

        public object PlannedEndDate { get; set; }

        public object PlannedStartDate { get; set; }

        public Priority Priority { get; set; }

        public double Progress { get; set; }

        public Project Project { get; set; }

        public Release Release { get; set; }

        public DateTime? StartDate { get; set; }

        public string Tags { get; set; }

        public object Team { get; set; }

        public object TeamIteration { get; set; }

        public double TimeRemain { get; set; }

        public double TimeSpent { get; set; }

        public UserStory UserStory { get; set; }

        public bool ShouldSerializeUserStory()
        {
            return false;
        }
       
    }

    public class Tasks
    {
        public List<Task> Items { get; set; }
    }

}