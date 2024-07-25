using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;

var listener = new HttpListener();

listener.Prefixes.Add("http://127.0.0.1:27001/");

listener.Start();

while (true)
{
    HttpListenerContext? context = await listener.GetContextAsync();

    HttpListenerRequest? request = context.Request;
    HttpListenerResponse? response = context.Response;

    response.ContentType = "text/html";
    response.StatusCode = 200;
    var writer = new StreamWriter(response.OutputStream);

    string url = request.RawUrl.Length > 0 ? request.RawUrl.Substring(1) : string.Empty;
    if (url.Equals("favicon.ico", StringComparison.OrdinalIgnoreCase))
    {

        writer.Dispose();
        continue;
    }
    var switchUrl = url.Split("/");
    List<string> turnUrlList = new List<string>(switchUrl);

    //string mainPath= "/Users/fermayilhesenov/Desktop/http_check";
    string mainPath = "/Users/fermayilhesenov/Projects/MultiCastServerSide/http_check";
    for (int i = 0; i < turnUrlList.Count; i++)
    {
        mainPath+= $"/{turnUrlList[i]}";
    }

    if (!mainPath.Contains(".html"))
    {
        mainPath += ".html";
    }

    Console.WriteLine($"Computed mainPath: {mainPath}");

   
    if (File.Exists($"{mainPath}"))
    {
        var text = File.ReadAllText(mainPath);
        writer.Write(text);
        Console.WriteLine("success");
    }
    else
    {
        var text = File.ReadAllText("/Users/fermayilhesenov/Desktop/http_check/404.html");
        response.StatusCode = 404;
        writer.Write(text);
    }
    writer.Dispose();
}