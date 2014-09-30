using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TargetBot.Models;
using System.Linq;

namespace TargetBot
{
    internal class TargetBot
    {
        private static EntityStates state;

        public static int DoneStoryStateId
        {
            get
            {
                return GetDoneStoryStateId(States);
            }
        }

        public static int ToBeTestedStoryStateId
        {
            get
            {
                return GetToBeTestStoryState(States);
            }
        }

        private static int DoneTaskStateId
        {
            get
            {
                return GetDoneTaskStateId(States);
            }
        }

        private static int InProgressStoryStateId
        {
            get
            {
                return GetInProgressStoryStateId(States);
            }
        }

        private static int OpenStoryStateId
        {
            get
            {
                return GetOpenStoryStateId(States);
            }
        }

        private static int OpenTaskStateId
        {
            get
            {
                return GetOpenTaskStateId(States);
            }
        }

        private static EntityStates States
        {
            get
            {
                if (state == null)
                {
                    state = JsonConvert.DeserializeObject<EntityStates>(TargetCommander.GetEntityStates());
                    return state;
                }
                return state;
            }
        }

        private static int ToBeTestedTaskStateId
        {
            get
            {
                return GetToBeTestTaskState(States);
            }
        }

        private static bool allTaskAreDone(Tasks tasks)
        {
            return tasks.Items.All(t => t.EntityState.Id == DoneTaskStateId);
        }

        private static bool AnyTaskNotOpen(Tasks tasks)
        {
            return tasks.Items.Any(t => t.EntityState.Id != OpenTaskStateId);
        }

        private static int GetDoneStoryStateId(EntityStates states)
        {
            return states.Items.Single(i => i.EntityType.Name.Equals("UserStory") && i.IsFinal).Id;
        }

        private static int GetDoneTaskStateId(EntityStates states)
        {
            return states.Items.Single(i => i.EntityType.Name.Equals("Task") && i.IsFinal).Id;
        }

        private static int GetInProgressStoryStateId(EntityStates states)
        {
            return states.Items.Single(i => i.EntityType.Name.Equals("UserStory") && i.Name.Equals("In Progress")).Id;
        }

        private static int GetOpenStoryStateId(EntityStates states)
        {
            return states.Items.Single(i => i.EntityType.Name.Equals("UserStory") && i.IsInitial).Id;
        }

        private static int GetOpenTaskStateId(EntityStates states)
        {
            return states.Items.Single(i => i.EntityType.Name.Equals("Task") && i.IsInitial).Id;
        }

        private static int GetToBeTestStoryState(EntityStates states)
        {
            return states.Items.Single(i => i.EntityType.Name.Equals("UserStory") && i.Name.Equals("To be tested")).Id;
        }

        private static int GetToBeTestTaskState(EntityStates states)
        {
            return states.Items.Single(i => i.EntityType.Name.Equals("Task") && i.Name.Equals("To be tested")).Id;
        }

        private static UserStories GetUserCorrectUserStoriesWithTask()
        {
            string TeamToFilterAway = "BI Team";
            var AllUserStories = JsonConvert.DeserializeObject<UserStories>(TargetCommander.GetStoriesForCurrentIteration());
            var filteredUserStories = new UserStories();
            filteredUserStories.Items = AllUserStories.Items.Where(i => (i.Team == null || !i.Team.Name.Equals(TeamToFilterAway)) && i.TasksCount > 0).ToList<UserStory>();
            return filteredUserStories;
        }

        private static void Main(string[] args)
        {
            UserStories userStories;
            while (true)
            {
                userStories = GetUserCorrectUserStoriesWithTask();
                foreach (var userStory in userStories.Items)
                {
                    Tasks tasks = JsonConvert.DeserializeObject<Tasks>(TargetCommander.GetTasksForUserStory(userStory.Id));
                    if (userStory.EntityState.Id == OpenStoryStateId && AnyTaskNotOpen(tasks))
                    {
                        Console.WriteLine("Opening UserStory {0} Wait...", userStory.Name);
                        userStory.EntityState.Id = InProgressStoryStateId;
                        TargetCommander.UpdateStory(userStory);
                    }
                    foreach (var task in tasks.Items)
                    {
                        if (task.EntityState.Id == ToBeTestedTaskStateId)
                        {
                            Console.WriteLine("Completing task {0} Wait...", task.Name);
                            task.EntityState.Id = DoneTaskStateId;
                            TargetCommander.UpdateTask(task);
                        }
                    }
                    if (userStory.EntityState.Id == InProgressStoryStateId && allTaskAreDone(tasks))
                    {
                        Console.WriteLine("Moving Story {0} to 'To be tested' Wait...", userStory.Name);
                        userStory.EntityState.Id = ToBeTestedStoryStateId;
                        TargetCommander.UpdateStory(userStory);
                    }
                }
                Thread.Sleep(30000);
            }
        }
    }
}