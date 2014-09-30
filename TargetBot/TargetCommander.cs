using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;
using RestSharp;
using TargetBot.Models;


namespace TargetBot
{
    internal static class TargetCommander
    {
        private static string baseUrl = "https://moravia.tpondemand.com";

        public static string GetAllBugsForUserStory(JToken story)
        {
            var resource = string.Format("/api/v1/Userstories/{0}?include=[Bugs]&format=json", story["Id"]);

            return SendGETRequest(resource).Content;
        }

        public static string GetBugState(int id)
        {
            var resource = string.Format("/api/v1/Bugs/{0}?include=[EntityState]&format=json", id);

            return SendGETRequest(resource).Content;
        }

        public static string GetEntityStates()
        {
            var resource = "/api/v1/EntityStates?format=json&where=Process.Name eq 'Scrum'";

            return SendGETRequest(resource).Content;
        }

        public static string GetStoriesForCurrentIteration()
        {
            var resource = "/api/v1/UserStories?where=Iteration.IsCurrent eq 'true'&format=json&append=[bugs-count,tasks-count]";

            return SendGETRequest(resource).Content;
        }

        public static string GetTasksForUserStory(int storyId)
        {
            var resource = string.Format("/api/v1/Tasks?where=UserStory.Id eq '{0}'&format=json", storyId);

            return SendGETRequest(resource).Content;
        }

        public static void UpdateStory(UserStory story)
        {
            var resource = string.Format("/api/v1/UserStories/{0}", story.Id);

            RestResponse response = SendPOSTRequest(resource, story);

            Console.WriteLine(string.Format("Updated story id:{0} and it was {1}", story.Id, response.StatusCode));
        }

        public static void UpdateTask(Task task)
        {
            var resource = string.Format("/api/v1/Tasks/{0}", task.Id);

            RestResponse response = SendPOSTRequest(resource, task);

            Console.WriteLine(string.Format("Updated task id:{0} and it was {1}", task.Id, response.StatusCode));
        }

        private static RestResponse ExecuteRequest(RestRequest request)
        {
            var client = new RestClient(baseUrl);
            client.Authenticator = new HttpBasicAuthenticator("jsteidl@moravia.com", "drf351gh");
            RestResponse response = client.Execute(request) as RestResponse;
            if (!response.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new HttpListenerException((int)response.StatusCode, response.StatusDescription);
            }
            return response;
        }

        private static RestResponse SendGETRequest(string resource)
        {
            var request = new RestRequest(resource, Method.GET);
            RestResponse response = ExecuteRequest(request);
            return response;
        }

        private static RestResponse SendPOSTRequest(string resource, object body)
        {
            var request = new RestRequest(resource, Method.POST);
            request.JsonSerializer = new JsonSerializer();
            request.RequestFormat = DataFormat.Json;
            request.AddBody(body);

            RestResponse response = ExecuteRequest(request);
            return response as RestResponse;
        }
    }
}