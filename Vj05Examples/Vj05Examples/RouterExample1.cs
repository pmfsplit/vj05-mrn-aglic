using System;
using Akka.Actor;
using Akka.Routing;

namespace Vj05Examples
{
    public class RouterExample1
    {
        public void Start()
        {
            using (var system = ActorSystem.Create("router-example"))
            {
                // Ne kreiramo MyActor
                // Kreiramo rutera koji će kreirati 5 MyActora 
                var props = Props.Create(() => new MyActor())
                    .WithRouter(new RoundRobinPool(5));

                var router = system.ActorOf(props);

                // RoundRobin -> 0, 1, 2, 3, 4, 0, 1, 2, 3, 4, 0, 1, 2, 3, 4, 0, 1, 2...
                
                for (int i = 0; i < 5; i++)
                {
                    router.Tell(new Messages.Ping(i));
                }

                Console.ReadLine();
            }
        }
    }
}