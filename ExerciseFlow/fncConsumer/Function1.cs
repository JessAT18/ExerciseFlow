namespace fncConsumer
{
    using System;
    using fncConsumer.Models;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System.Net.Mail;
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run(
            [ServiceBusTrigger(
                    "queueejercicios",
                    Connection = "MyConn"
            )] string myQueueItem,

            ILogger log
        )
        {
            try
            {
                log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
                var dataOdometer = JsonConvert.DeserializeObject<Odometer>(myQueueItem);
                //Aqui va la accion await datos.AddAsync(data);
                MailMessage message = new MailMessage();
                message.To.Add(dataOdometer.Email); //Email from queue
                message.Subject = "YourOdometer";
                message.From = new MailAddress("jessat18si410@outlook.com"); //My Email
                message.Body = $"Hola! {dataOdometer.Name}, desde la última toma en {dataOdometer.Datetime}, caminaste {dataOdometer.Steps} pasos.";

                SmtpClient smtp = new SmtpClient("smtp-mail.outlook.com");
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("jessat18si410@outlook.com", "myPassw0rd123");
                smtp.EnableSsl = true;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                log.LogError($"It is not possible to send mails: {ex.Message}");
            }
        }
    }
}
