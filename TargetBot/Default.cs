using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading;

namespace TargetBot
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] storyIds;
            int[][] tasks;
            List<JToken> sortedStories;
            int openStateStory = 46;
            int inProgressStateStory = 47;
            int testingStateStory = 48;
            int inProgressStateTask = 51;
            int doneStateTask = 52;
            
           // JObject  taskInfo = JsonManipulator.createStories(TargetCommander.GetTaskState(Convert.ToInt32(sortedStories[0]["Tasks"]["Items"][0]["Id"])));
            //Console.WriteLine(taskInfo["EntityState"]["Id"]);
            //while (true)
            //{
                JObject storiesRaw = JsonManipulator.createStories(TargetCommander.GetStories());
                sortedStories = JsonManipulator.sortStories(storiesRaw);
                foreach (var story in sortedStories)
                {
                    if (Convert.ToInt32(story["EntityState"]["Id"]) == openStateStory || Convert.ToInt32(story["EntityState"]["Id"]) == inProgressStateStory)
                    {
                        updateStories(story);
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
            //}
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
        static void updateStories(JToken story)
        {
        }
    }
}

