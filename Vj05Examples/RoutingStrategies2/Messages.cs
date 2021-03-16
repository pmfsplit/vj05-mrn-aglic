using System;

namespace RoutingStrategies2
{
    // poruka za consistenthashing ruter primjer
    class CurrencyPair
    {
        public string Pair { get; }
        public double Price { get; }

        public CurrencyPair(string pair, double price)
        {
            Pair = pair;
            Price = price;
        }
    }

    
    // Request i Reply su poruke za ScatterGatherFirstCompleted router
    class Go // poruka Go je isključico za započet sa ScatterGatherFirstCompleted
    {
        
    }

    class Request
    {
        public string Content { get; }
        public Random Rnd { get; }

        public Request(string content, Random rnd)
        {
            Rnd = rnd;
            Content = content;
        }
    }

    class Reply
    {
        public string Sender { get; }
        public string Content { get; }
        public DateTime Created { get; }

        public Reply(string content, string sender)
        {
            Content = content;
            Sender = sender;
            Created = DateTime.Now;
        }
    }

    class Msg
    {
        public string Content { get; }

        public Msg(string content)
        {
            Content = content;
        }
    }

    public class Data
    {
        public string Receiver { get; }
        public string Sender { get; }

        public Data(string sender, string receiver)
        {
            Sender = sender;
            Receiver = receiver;
        }
    }

    public class Print
    {
    }
}