using Signal_R.Models;
using System.Collections.Concurrent;

namespace Signal_R.Data
{
    public class SharedDb
    {
        private readonly ConcurrentDictionary<string, UserConnection> _connections = new ConcurrentDictionary<string, UserConnection>();
        public  ConcurrentDictionary<string, UserConnection> Connections => _connections;
    }
}
