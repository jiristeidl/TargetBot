using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace TargetBot
{
    class Program
    {
        static void Main(string[] args)
        {
            List<JToken> sortedStories;
            List<JToken> sortedTasks;

            int openStateStory = 46;
            int inProgressStateStory = 47;
            int testingStateStory = 48;

            while (true)
            {
                JObject storiesRaw = JsonManipulator.createStories(TargetCommander.GetStories());
                sortedStories = JsonManipulator.sortStories(storiesRaw);
                foreach (var story in sortedStories)
                {
                    if (Convert.ToInt32(story["EntityState"]["Id"]) == openStateStory || Convert.ToInt32(story["EntityState"]["Id"]) == inProgressStateStory)
                    {
                        sortedTasks = JsonManipulator.sortTasks(story);
                        foreach (var task in sortedTasks)
                        {
                            if (Convert.ToInt32(story["EntityState"]["Id"]) == openStateStory)
                            {
                                if (taskIsInProgress(task))
                                {
                                    TargetCommander.UpdateStoryState(JsonManipulator.createJsonForStateChange(inProgressStateStory), Convert.ToInt32(story["Id"]));
                                }
                            }
                        }
                        if (Convert.ToInt32(story["EntityState"]["Id"]) == inProgressStateStory)
                        {                            
                            if (allTasksAreDone(sortedTasks) && noOpenBugs(story))
                            {
                                TargetCommander.UpdateStoryState(JsonManipulator.createJsonForStateChange(testingStateStory), Convert.ToInt32(story["Id"]));
                            }
                        }
                    }
                }
                Thread.Sleep(1000);
            }
        }
        static int[] selectIds(List<JToken> list)
        {
            int length = list.Count;
            int[] ids = new int[length];
            for (int i = 0; i < length; i++)
            {
                ids[i] = Convert.ToInt32(list[i]["Id"]);                
            }            
            return ids;
        }
        static bool taskIsInProgress(JToken task)
        {            
            bool change = false;
            int openState = 50;

            JObject state = JsonManipulator.createStories(TargetCommander.GetTaskState(Convert.ToInt32(task["Id"])));
            if (Convert.ToInt32(state["EntityState"]["Id"]) != openState) change = true;            
            return change;
        }
        static bool allTasksAreDone(List<JToken> tasks)
        {
            BitArray tasksDone = new BitArray(tasks.Count, false);
            int doneStateTask = 52;
            for (int i=0; i < tasks.Count;i++ )
            {
                JObject task = JsonManipulator.createStories(TargetCommander.GetTaskState(Convert.ToInt32(tasks[i]["Id"])));                
                if (Convert.ToInt32(task["EntityState"]["Id"]) == doneStateTask)
                {
                    tasksDone[i]=true;
                }
            }
            foreach (bool value in tasksDone)
            {
                if (!value) return false;
            }
            return true;
        }
        static bool noOpenBugs(JToken story)
        {
            JObject bugs = JsonManipulator.createStories(TargetCommander.GetAllBugsId(story));
            if (bugs["Bugs"]["Items"].ToString() == "[]")
            {
                return true;
            }
            return false;
        }        
    }
}

