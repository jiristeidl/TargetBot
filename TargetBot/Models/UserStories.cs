using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace TargetBot.Models
{
    public class EntityType
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class Feature
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class Iteration
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class Owner
    {
        public string FirstName { get; set; }

        public int Id { get; set; }

        public string LastName { get; set; }
    }

    public class Priority
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class Release
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class Team
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class UserStories
    {
        public List<UserStory> Items { get; set; }
    }

    public class UserStory
    {
        [JsonProperty("Bugs-Count")]
        public int BugsCount { get; set; }

        public DateTime CreateDate { get; set; }

        public List<object> CustomFields { get; set; }

        public object Description { get; set; }

        public double Effort { get; set; }

        public double EffortCompleted { get; set; }

        public double EffortToDo { get; set; }

        public object EndDate { get; set; }

        public EntityState EntityState { get; set; }

        public EntityType EntityType { get; set; }

        public Feature Feature { get; set; }

        public int Id { get; set; }

        public double InitialEstimate { get; set; }

        public Iteration Iteration { get; set; }

        public object LastCommentDate { get; set; }

        public object LastCommentedUser { get; set; }

        public DateTime ModifyDate { get; set; }

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

        [JsonProperty("Tasks-Count")]
        public int TasksCount { get; set; }

        public Team Team { get; set; }

        public object TeamIteration { get; set; }

        public double TimeRemain { get; set; }

        public double TimeSpent { get; set; }

        public bool ShouldSerializeBugsCount()
        {
            return false;
        }

        public bool ShouldSerializeTasksCount()
        {
            return false;
        }
    }
}