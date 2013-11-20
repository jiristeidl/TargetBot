using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TargetBot
{
class Program
{
    static void Main(string[] args)
    {
        // change the port number accordingly for this to work, when your cassini starts up.
        WebRequest req = WebRequest.Create(@"http://sauge.tpondemand.com//api/v1/UserStories/231?resultFormat=json");
        req.Credentials = new NetworkCredential("admin", "admin");

        req.Method = "POST";
        req.ContentType = @"application/json; charset=utf-8";
        WriteProductXml(req, 47);

        HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
        Console.WriteLine(string.Format("Status Code: {0}, Status Description: {1}", resp.StatusCode, resp.StatusDescription));
        Console.Read();
    }

    public static void WriteProductXml(WebRequest req, int id)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("{EntityState:{Id:" + id +"}}");
        Debug.WriteLine(builder.ToString());        
        req.ContentLength = Encoding.UTF8.GetByteCount(builder.ToString());

        using (Stream stream = req.GetRequestStream())
        {
            stream.Write(Encoding.UTF8.GetBytes(builder.ToString()), 0, Encoding.UTF8.GetByteCount(builder.ToString()));
        }
    }
}
}
