using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace TargetBot
{
    static class TargetCommander
    {
        
        static NetworkCredential auth = new NetworkCredential("admin", "admin");
        static string baseUrl = "http://sauge.tpondemand.com";

        public static string GetStories()
        {
            Console.WriteLine("Retrieving UserStories in current Iteration");
            WebRequest req = WebRequest.Create(baseUrl + "/api/v1/UserStories?where=Iteration.IsCurrent eq 'true'&include=[Id,Tasks,EntityState]&format=json");
            req.Credentials = auth;
            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                using (Stream respStream = resp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream, Encoding.UTF8);
                    Console.WriteLine("Success!");
                    return reader.ReadToEnd();
                }
            }
            else
            {
                Console.WriteLine(string.Format("Status Code: {0}, Status Description: {1}", resp.StatusCode, resp.StatusDescription));
                return null;
            }            
        }
        public static string GetTaskState(int id)
        {
            Console.WriteLine("Retrieving state for task " + id);
            WebRequest req = WebRequest.Create(baseUrl + "/api/v1/Tasks/" + id + "?include=[EntityState]&format=json");
            req.Credentials = auth;
            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                using (Stream respStream = resp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream, Encoding.UTF8);
                    Console.WriteLine("Success!");
                    return reader.ReadToEnd();
                }
            }
            else
            {
                Console.WriteLine(string.Format("Status Code: {0}, Status Description: {1}", resp.StatusCode, resp.StatusDescription));
                return null;
            }            

        }
        public static string GetBugState(int id)
        {
            Console.WriteLine("Retrieving state for bug " + id);
            WebRequest req = WebRequest.Create(baseUrl + "/api/v1/Tasks/" + id + "?include=[EntityState]&format=json");
            req.Credentials = auth;
            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                using (Stream respStream = resp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream, Encoding.UTF8);
                    Console.WriteLine("Success!");
                    return reader.ReadToEnd();
                }
            }
            else
            {
                Console.WriteLine(string.Format("Status Code: {0}, Status Description: {1}", resp.StatusCode, resp.StatusDescription));
                return null;
            }            
        }
        public static void UpdateStoryState(JObject json, int id)
        {
            Console.WriteLine("Updating story " + id);
            WebRequest req = WebRequest.Create(baseUrl + "/api/v1/UserStories/" + id);
            req.Credentials = auth;
            req.Method = "POST";
            req.ContentType = @"application/json; charset=utf-8";
            req.ContentLength = Encoding.UTF8.GetByteCount(json.ToString());
            using (Stream stream = req.GetRequestStream())
            {
                stream.Write(Encoding.UTF8.GetBytes(json.ToString()), 0, Encoding.UTF8.GetByteCount(json.ToString()));
            }
            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            Console.WriteLine(string.Format("Status Code: {0}, Status Description: {1}", resp.StatusCode, resp.StatusDescription));            
        }

    }
}
