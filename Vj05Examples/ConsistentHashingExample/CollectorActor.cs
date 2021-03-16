using System;
using System.Collections.Generic;
using Akka.Actor;

namespace ConsistentHashingExample
{
    public class CollectorActor : ReceiveActor
    {
        private List<int> History { get; }
        public CollectorActor()
        {
            History = new List<int>();
            Receive<Messages.Content>(x => Store(x));
            Receive<Messages.Print>(x => Print());
        }

        private void Print()
        {
            Console.WriteLine(Self.Path);
            History.ForEach(x => Console.Write($"{x} "));
            Console.WriteLine();
            Sender.Tell("Done!");
        }
        
        private void Store(Messages.Content data)
        {
            History.Add(data.Num);
        }
    }
}