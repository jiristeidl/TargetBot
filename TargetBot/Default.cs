﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;

namespace TargetBot
{
    class Program
    {
        static void Main(string[] args)
        {
            UserStory test = JsonConvert.DeserializeObject<UserStory>(TargetCommander.GetStories());
            Console.WriteLine(test.EntityState);
            
        }


    }
}

