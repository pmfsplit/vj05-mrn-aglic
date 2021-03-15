namespace HoconPrimjer
{
    public class Messages
    {
        public class Ping
        {
            public int IdPoruke { get; }

            public Ping(int idPoruke)
            {
                IdPoruke = idPoruke;
            }
        }

        public class Empty
        {
        }

        public class Print
        {
        }
    }
}