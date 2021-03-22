using System;
using Akka.Actor;

namespace PubSubPrimjer
{
    public class PricedQuote
    {
    }

    public class Price
    {
    }

    public class QuoteListener : ReceiveActor
    {
        public QuoteListener()
        {
            Receive<PricedQuote>(x => Console.WriteLine($"{Self.Path} Dobio price quote"));
        }
    }

    public class PriceListener : ReceiveActor
    {
        public PriceListener()
        {
            Receive<PricedQuote>(x => Console.WriteLine(x));
            Receive<Price>(x => Console.WriteLine($"{Self.Path} Dobio price"));
        }
    }

    public class TestActor : ReceiveActor
    {
        public TestActor()
        {
            Receive<MsgEnvelope>(x => Console.WriteLine($"{Self.Path}: {x.Payload}"));
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            // using (var system = ActorSystem.Create("pubsub"))
            // {
            //     var quoteListener = system.ActorOf(Props.Create(() => new QuoteListener()), "quotelistener");
            //     var priceListener = system.ActorOf(Props.Create(() => new PriceListener()), "pricelistener");
            //
            //     system.EventStream.Subscribe(quoteListener, typeof(PricedQuote));
            //     system.EventStream.Subscribe(priceListener, typeof(PricedQuote));
            //     system.EventStream.Subscribe(priceListener, typeof(Price));
            //
            //     system.EventStream.Publish(new PricedQuote());
            //     system.EventStream.Publish(new Price());
            //     system.EventStream.Publish(new PricedQuote());
            //
            //     
            //     // system.Terminate().Wait();
            //     
            //     system.WhenTerminated.Wait();
            // }
            using (var system = ActorSystem.Create("pubsub"))
            {
                var bus = new LookupBus();
                var testActor = system.ActorOf(Props.Create(() => new TestActor()));
                
                // bus.Subscribe(testActor, "greetings");
                bus.Subscribe(testActor, "sport");
                bus.Publish(new MsgEnvelope("greetings", "hello"));
                bus.Publish(new MsgEnvelope("sport", "Hajduk izgubio od Gorice"));
                // bus.Publish("Hello world");
                
                system.WhenTerminated.Wait();
            }
        }
    }
}