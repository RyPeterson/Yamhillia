using System;
using YamhilliaNET.Data;

namespace YamhilliaNET.Services
{
    public class ServerService: IServerService
    {
        private readonly YamhilliaContext _db;

        public ServerService(YamhilliaContext db)
        {
            _db = db;
        }


        public bool Ping()
        {
            try
            {
                return _db.Database.CanConnect();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}