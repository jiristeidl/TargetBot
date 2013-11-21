using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace TargetBot
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] storyIds;
            List<JToken> sortedStories;
            JObject storiesRaw = JsonManipulator.createStories(TargetCommander.GetStories());
            sortedStories = JsonManipulator.sortStories(storiesRaw);
            storyIds = selectIds(sortedStories);
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
    }
}

