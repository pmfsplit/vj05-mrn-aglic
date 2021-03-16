using System;
using System.Threading;
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

        protected override void PreStart()
        {
            Console.WriteLine(Self.Path);
            base.PreStart();
        }

        protected override void PostStop()
        {
            Console.WriteLine($"Gašenje: {Self.Path}");
            base.PostStop();
        }
    }
}