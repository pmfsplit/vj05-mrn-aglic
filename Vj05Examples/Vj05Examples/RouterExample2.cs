using System;
using System.Linq;
using System.Threading;
using Akka.Actor;
using Akka.Routing;

namespace Vj05Examples
{
    public class RouterExample2
    {
        private void Send1000Messages(IActorRef actor)
        {
            foreach (var el in Enumerable.Range(0, 1000))
            {
                actor.Tell(new Messages.Empty());
            }
        }
        public void Start()
        {
            using (var system = ActorSystem.Create("router-examples"))
            {
                var broadcastProps = Props.Create(() => new CollectorActor())
                    .WithRouter(new BroadcastPool(5));
                var broadcastRouter = system.ActorOf(broadcastProps);
                
                Console.WriteLine("Broadcast:");
                Send1000Messages(broadcastRouter);
               
                Thread.Sleep(3000);
                broadcastRouter.Tell(new Messages.Print());
                Thread.Sleep(1000);
                
                // system.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(2), broadcastRouter, new Messages.Print(), system.DeadLetters);
                
                var randomProps = Props.Create(() => new CollectorActor())
                    .WithRouter(new RandomPool(5));
                var randomRouter = system.ActorOf(randomProps);
                
                Console.WriteLine("Random:");
                Send1000Messages(randomRouter);
               
                Thread.Sleep(3000);
                randomRouter.Tell( new Broadcast( new Messages.Print()));
                Thread.Sleep(1000);


                var roundRobinProps = Props.Create(() => new CollectorActor())
                    .WithRouter(new RoundRobinPool(5));
                var roundRobinRouter = system.ActorOf(roundRobinProps);
                
                Console.WriteLine("Round robin:");
                Send1000Messages(roundRobinRouter);
               
                Thread.Sleep(3000);
                roundRobinRouter.Tell(new Broadcast( new Messages.Print()));
                Thread.Sleep(1000);

                var smallestMailboxProps = Props.Create(() => new CollectorActor())
                    .WithRouter(new SmallestMailboxPool(5));
                var smallestMailboxRouter = system.ActorOf(smallestMailboxProps);
                
                Console.WriteLine("Smallest mailbox:");
                Send1000Messages(smallestMailboxRouter);
                
               
                Thread.Sleep(3000);
                smallestMailboxRouter.Tell(new Broadcast(new Messages.Print()));
            }
        }
    }
}