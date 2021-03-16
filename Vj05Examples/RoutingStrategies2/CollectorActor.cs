using System;
using System.Collections.Generic;
using Akka.Actor;

namespace RoutingStrategies2
{
    public class CollectorActor : ReceiveActor
    {
        private Dictionary<string, int> _collected;

        public CollectorActor()
        {
            _collected = new Dictionary<string, int>();
            Receive<Data>(x => Collect(x));
            Receive<Print>(x => Print());
        }

        private void Print()
        {
            var sum = 0;
            foreach (var pair in _collected)
            {
                Console.WriteLine($"{pair.Key}: {pair.Value}");
                sum = sum + pair.Value;
            }
            Console.WriteLine($"# messages: {sum}");
            _collected = new Dictionary<string, int>();
        }

        private void Collect(Data x)
        {
            if (_collected.ContainsKey(x.Receiver))
            {
                _collected[x.Receiver]++;
            }
            else
            {
                _collected.Add(x.Receiver, 1);
            }
        }
    }
}