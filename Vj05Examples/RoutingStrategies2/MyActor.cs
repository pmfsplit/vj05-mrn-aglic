using System;
using System.Threading;
using Akka.Actor;
using Akka.Routing;

namespace RoutingStrategies2
{
    public class MyActor : ReceiveActor
    {
        public MyActor()
        {            
            Receive<Request>(x =>
            {
                var self = Self.Path.ToString();
                var wait = x.Rnd.Next(0, 4000);
                var seconds = TimeSpan.FromMilliseconds(wait);
                Console.WriteLine($"{self} got message: {x.Content}, waiting: {seconds}");
                Thread.Sleep(seconds);
                Sender.Tell(new Reply(x.Content, self));
            });
        }
    }
    
    
    // Za laksu demonstraciju ScatterGatherFirstCompleted routera
    public class ScatterGatherFirstCompletedActor : ReceiveActor
    {
        private static void SendSGMessages(string message, IActorRef router)
        {
            Console.WriteLine(message);
            
            router.Tell(new Request("Sadržaj... bla bla bla", new Random()));
        }

        public ScatterGatherFirstCompletedActor()
        {
            Receive<Go>(x =>
            {
                // ScatterGatherFirstCompleted router
                var within = TimeSpan.FromSeconds(2);

                var props = Props.Create(() => new MyActor())
                    .WithRouter(new ScatterGatherFirstCompletedPool(5, within)); 
                var childRouter = Context.ActorOf(props);
                SendSGMessages("ScatterGatherFirstCompleted", childRouter);
            });
            
            // uočite da je sender koji odgovara deadletters
            Receive<Reply>(x => Console.WriteLine($"Dobio odgovor od: ${x.Sender} (Akka sender: {Sender})"));
        }
    }
    
    // Za laksu demonstraciju TailChopping routera
    public class TailChoppingActor : ReceiveActor
    {
        private static void SendTCMessages(string message, IActorRef router)
        {
            Console.WriteLine(message);
            
            router.Tell(new Request("Sadržaj... bla bla bla", new Random()));
        }

        public TailChoppingActor()
        {
            Receive<Go>(x =>
            {
                // ScatterGatherFirstCompleted router
                var within = TimeSpan.FromSeconds(5);
                var interval = TimeSpan.FromSeconds(1);

                var props = Props.Create(() => new MyActor())
                    .WithRouter(new TailChoppingPool(5, within, interval)); 
                var childRouter = Context.ActorOf(props);
                SendTCMessages("TailChopping", childRouter);

                // var paths = new List<string>();
                // foreach (var _ in Enumerable.Range(0, 5))
                // {
                //     var propsLocal = Props.Create(() => new MyActor(collector));
                //     var child = Context.ActorOf(propsLocal);
                //     paths.Add(child.Path.ToString());
                // }
                //
                // var props2 = Props.Create(() => new MyActor(collector))
                //     .WithRouter(new TailChoppingGroup(paths, within, interval)); 
                // var childRouterGroup = Context.ActorOf(props2, "GrupoMoja");
                // SendTCMessages("TailChopping", childRouterGroup);
            });

            // uočite da je sender koji odgovara deadletters
            Receive<Reply>(x => Console.WriteLine($"Dobio odgovor od: ${x.Sender} (Akka sender: {Sender})"));
        }
    }
}