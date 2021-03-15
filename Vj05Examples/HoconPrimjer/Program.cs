using System.Linq;
using System.Threading;
using Akka.Actor;
using Akka.Routing;

namespace HoconPrimjer
{
    public class Program
    {
        private static void Send1000Messages(IActorRef actor)
        {
            foreach (var el in Enumerable.Range(0, 1000))
            {
                actor.Tell(new Messages.Empty());
            }
        }
        
        public static void Main(string[] args)
        {
            using (var system = ActorSystem.Create("hocon-primjer"))
            {
                var props = Props.Create(() => new CollectorActor())
                    .WithRouter(FromConfig.Instance);
                var router = system.ActorOf(props, "rutko");
                
                Send1000Messages(router);
                Thread.Sleep(3000);
                router.Tell(new Broadcast(new Messages.Print()));
            }
        }
    }
}