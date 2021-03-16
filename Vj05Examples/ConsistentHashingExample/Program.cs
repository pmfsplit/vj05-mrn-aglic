using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using Akka.Routing;

namespace ConsistentHashingExample
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var system = ActorSystem.Create("consistent-hashing"))
            {
                var props = Props.Create(() => new CollectorActor()).WithRouter(new ConsistentHashingPool(5));
                var router = system.ActorOf(props);

                router.Tell(new Messages.Content(4));
                router.Tell(new Messages.Content(14));
                router.Tell(new Messages.Content(5));
                router.Tell(new Messages.Content(0));
                router.Tell(new Messages.Content(15));
                router.Tell(new Messages.Content(23));
                router.Tell(new Messages.Content(23));
                router.Tell(new Messages.Content(23));
                router.Tell(new Messages.Content(23));
                router.Tell(new Messages.Content(23));
                router.Tell(new Messages.Content(23));
                router.Tell(new Messages.Content(22));
                router.Tell(new Messages.Content(20));
                router.Tell(new Messages.Content(21));

                foreach (var i in Enumerable.Range(0, 5))
                {
                    int j = i;
                    var result = router.Ask<string>(new ConsistentHashableEnvelope(new Messages.Print(), j % 5))
                        .Result;
                    Console.WriteLine($"{result}");
                }
            }
        }
    }
}