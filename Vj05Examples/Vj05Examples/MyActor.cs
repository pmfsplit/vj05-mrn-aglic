using System;
using System.Threading;
using Akka.Actor;

namespace Vj05Examples
{
    class MyActor : ReceiveActor
    {
        private int brojac = 0;
        public MyActor()
        {
            Receive<Messages.Ping>(x =>
            {
                brojac++;
                Console.WriteLine($"{Self.Path} primio: {brojac} poruka. Ova poruka ima id: {x.IdPoruke}." +
                                  $"Na niti: {Thread.CurrentThread.ManagedThreadId}");
            });
        }
    }
}