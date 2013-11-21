using Newtonsoft.Json.Linq;
using System;

namespace TargetBot
{
    class Program
    {
        static void Main(string[] args)
        {
            
            JObject test = JObject.Parse(TargetCommander.GetTaskState(238));
            Console.WriteLine(test);
            

            
            
        }


    }
}

