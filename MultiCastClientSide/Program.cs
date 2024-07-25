

//---------------------Serverde yazdigimiz kodun client hissesi

var client = new HttpClient();
var result = await client.GetStringAsync("http://127.0.0.1:27001/adada/adadada/adad?name=cavid&age=23");
Console.WriteLine(result);
