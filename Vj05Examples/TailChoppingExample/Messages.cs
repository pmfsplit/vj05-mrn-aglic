namespace TailChoppingExample
{
    public class Messages
    {
        public class Request
        {
        }

        public class Reply
        {
            public string Name { get; }

            public Reply(string name)
            {
                Name = name;
            }
        }
    }
}