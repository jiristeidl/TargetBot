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
            storyIds = selectIds(sortedStories);
            
            
            

            

            
            
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
            int length = list.Count;
            int[] ids = new int[length];
            for (int i = 0; i < length; i++)
            {
                ids[i] = Convert.ToInt32(list[i]["Id"]);
                Console.WriteLine(ids[i]);
            }
            
            return ids;
        }


    }
}

