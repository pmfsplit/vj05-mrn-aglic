using Akka.Actor;

namespace RoutingStrategies2
{
    class Program
    {
        public static void Main(string[] args)
        {
            using (var system = ActorSystem.Create("RoutingStrategies2"))
            {
               
//                // za ScatterGatherFirstCompleted primjer:
//                var propsSGFC = Props.Create(() => new ScatterGatherFirstCompletedActor(collector));
//                system.ActorOf(propsSGFC).Tell(new Go());
//                
//                Thread.Sleep(3000);
//                
                var propsTailChopping = Props.Create(() => new TailChoppingActor());
                system.ActorOf(propsTailChopping).Tell(new Go());
                
                system.WhenTerminated.Wait();
            }
        }
    }
}