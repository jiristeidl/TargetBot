using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargetBot
{
    static class JsonManipulator
    {
        public static JObject createStories(string json)
        {
            return JObject.Parse(json);
        }
        public static string createJson(JObject json)
        {
            return json.ToString();
        }
        public static List<JToken> sortStories(JObject stories)
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
        public static JObject createJsonForStateChange(int state)
        {
            JObject entity = new JObject(
                new JProperty("EntityState",
                    new JObject(
                        new JProperty("Id", state.ToString()))));
            return entity;
        }
    }
}
