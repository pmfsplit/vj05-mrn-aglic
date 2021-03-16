using System;
using Akka.Actor;
using Akka.Routing;

namespace TailChoppingExample // i first Completed
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var system = ActorSystem.Create("komplicirane-strategije"))
            {
                var random = new Random();

                var within = TimeSpan.FromSeconds(20); // koliko dugo cekamo na odgovor
                var interval = TimeSpan.FromSeconds(1); // interval kojim saljemo novu poruku
                
                var props = Props.Create(() => new MyActor(random))
                    .WithRouter(new TailChoppingPool(5, within, interval));
                var router = system.ActorOf(props);
                
                // router.Tell(new Messages.Request());

                var name = router.Ask<Messages.Reply>(new Messages.Request()).Result.Name;
                Console.WriteLine($"Dobio odgovor od: {name}");
                    //.ContinueWith(x => Console.WriteLine(x.Result.Name));
                
                Console.ReadLine();
            }
        }
    }
}