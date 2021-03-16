using Akka.Routing;

namespace ConsistentHashingExample
{
    public class Messages
    {
        public class Content : IConsistentHashable
        {
            public int Num { get; }

            public Content(int num)
            {
                Num = num;
            }

            public object ConsistentHashKey
            {
                get { return Num % 5; }
            }
        }

        public class Print
        {
        }
    }
}