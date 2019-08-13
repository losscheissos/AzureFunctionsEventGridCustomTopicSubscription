using System;
using System.Threading.Tasks;

namespace Producer
{
    class Program
    {
        private const string EVENTGRID_ENDPOINT = "";
        private const string EVENTGRID_KEY = "";

        static async Task Main(string[] args)
        {
            Console.WriteLine("Send Sendgrid Event");

            var sender = new EventGrid.EventGridSender(() => EVENTGRID_ENDPOINT, () => EVENTGRID_KEY);
            await sender.SendSingleEventToTopic(new EventGrid.EventGridEvent<SampleEvent>()
            {
                Data = new SampleEvent()
                {
                    Message = "This is a sample Event"
                }
            });

            Console.WriteLine("Event sent");
        }

        class SampleEvent
        {
            public string Message;
        }
    }
}
