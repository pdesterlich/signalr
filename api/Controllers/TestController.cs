using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;

namespace api.Controllers
{
    [Route("api/test")]
    public class TestController: Controller
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MessageViewModel model)
        {
            Console.WriteLine($"ricevuto: {model.Messaggio}");
            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5001/chat")
                .WithConsoleLogger()
                .Build();

            connection.On<string>("Send", data =>
            {

            });
            
            await connection.StartAsync();

            await connection.InvokeAsync("Send", model.Messaggio);

            return Ok();
        }
        
        [HttpPost("group/{groupName}")]
        public async Task<IActionResult> PostGroup(string groupName, [FromBody] MessageViewModel model)
        {
            Console.WriteLine($"ricevuto: {model.Messaggio}");
            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5001/chat")
                .WithConsoleLogger()
                .Build();

            connection.On<string>("Send", data =>
            {

            });
            
            await connection.StartAsync();

            await connection.InvokeAsync("SendGroup", groupName, model.Messaggio);

            return Ok();
        }
    }

    public class MessageViewModel
    {
        public string Messaggio { get; set; }
    }
}