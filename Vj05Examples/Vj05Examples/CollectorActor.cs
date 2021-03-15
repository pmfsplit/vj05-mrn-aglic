using System;
using Akka.Actor;

namespace Vj05Examples
{
    public class CollectorActor : ReceiveActor
    {
        public CollectorActor()
        {
            int br = 0;
            Receive<Messages.Empty>(x => br++);
            Receive<Messages.Print>(x => Console.WriteLine($"{Self.Path} obradio: {br} poruka"));
        }
    }
}