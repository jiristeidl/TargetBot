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
        

    }
}
