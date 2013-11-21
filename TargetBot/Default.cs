using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace TargetBot
{
    class Program
    {
        static void Main(string[] args)
        {            
            int stories = 0;
            int[] storyIds;
            List<JToken> sortedStories;
            JObject storiesRaw = JsonManipulator.createStories(TargetCommander.GetStories());
            sortedStories = sortStories(storiesRaw);
            
            

            

            
            
        }
        static List<JToken> sortStories(JObject stories)
        {
            List<JToken> storiesWithTasks = new List<JToken>();
            foreach (var story in stories["Items"])
            {
                
                //Console.WriteLine(story["Tasks"]["Items"]);
                if (story["Tasks"]["Items"].ToString() != "[]")
                {
                    storiesWithTasks.Add(story);
                }
            }
            return storiesWithTasks;
        }
        static int[] selectIds(List<JToken> list)
        {
            return new int[5];
        }


    }
}

