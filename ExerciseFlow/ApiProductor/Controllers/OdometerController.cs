namespace ApiProductor.Controllers
{
    using ApiProductor.Models;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System;
    using System.Threading.Tasks;

    using Azure.Messaging.ServiceBus;

    [Route("api/[controller]")]
    [ApiController]
    public class OdometerController : ControllerBase
    {
        [HttpPost]
        public async Task<bool> EnviarAsync([FromBody] Odometer odometer)
        {
            string connectionString = "Endpoint=sb://queuejess.servicebus.windows.net/;SharedAccessKeyName=Enviar;SharedAccessKey=sNf2UfOxLet90gXq03ChFxRaGL/cqy4avqGAVfKVSCk=;EntityPath=queueejercicios";
            string queueName = "queueejercicios";
            string mensaje = JsonConvert.SerializeObject(odometer);
            await using (ServiceBusClient client = new ServiceBusClient(connectionString))
            {
                // create a sender for the queue 
                ServiceBusSender sender = client.CreateSender(queueName);

                // create a message that we can send
                ServiceBusMessage message = new ServiceBusMessage(mensaje);

                // send the message
                await sender.SendMessageAsync(message);
                Console.WriteLine($"Sent a single message to the queue: {queueName}");
            }
            return true;
        }
    }
}
