using System;
using System.Threading;
using Akka.Actor;

namespace TailChoppingExample
{
    public class MyActor : ReceiveActor
    {
        private readonly Random _rnd;

        public MyActor(Random rnd)
        {
            _rnd = rnd;
            Receive<Messages.Request>(_ => ObradiRequest());
        }

        private void ObradiRequest()
        {
            Console.WriteLine($"{Self.Path} prije spavanja....");
            Thread.Sleep(TimeSpan.FromSeconds(_rnd.Next(10)));
            Console.WriteLine($"{Self.Path} dobio request");
            // Thread.Sleep(_rnd.Next(100));
            Sender.Tell(new Messages.Reply(Self.Path.Name));
        }
    }
}