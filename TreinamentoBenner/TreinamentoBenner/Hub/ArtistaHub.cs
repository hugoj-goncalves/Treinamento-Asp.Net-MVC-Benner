using System.Collections.Concurrent;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using TreinamentoBenner.Core.Context;
using TreinamentoBenner.Core.Model;

namespace TreinamentoBenner.Hub
{
    public class ArtistaHub : Microsoft.AspNet.SignalR.Hub
    {
        private readonly TreinamentoBennerContext db = new TreinamentoBennerContext();
        public static readonly ConcurrentDictionary<string, int> Locks = new ConcurrentDictionary<string, int>();
        private static readonly object Lock = new object();

        public override async Task OnConnected()
        {
            var artistas = db.Artistas.OrderBy(q => q.Nome);

            await Clients.Caller.all(artistas);
            await Clients.Caller.allLocks(Locks.Values);
        }

        public override async Task OnReconnected()
        {
            await OnConnected();
        }

        public override async Task OnDisconnected(bool stopCalled)
        {
            int removed;
            if (Locks.TryRemove(Context.ConnectionId, out removed))
            {
                await Clients.All.allLocks(Locks.Values);
            }
        }

        public void TakeLock(Artista artista)
        {
            lock (Lock)
            {
                if (Locks.Values.Any(id => artista.Id == id))
                {
                    return;
                }

                Locks.AddOrUpdate(Context.ConnectionId, artista.Id, (key, oldValue) => artista.Id);
                Clients.Caller.takeLockSuccess(artista);
                Clients.All.allLocks(Locks.Values);
            }
        }

        public void Add(Artista artista)
        {
            db.Artistas.Add(artista);
            db.SaveChanges();

            Clients.All.add(artista);
        }

        public void Delete(Artista artista)
        {
            db.Artistas.Remove(db.Artistas.Find(artista.Id));
            db.SaveChanges();

            Clients.All.delete(artista);
        }

        public void Update(Artista artista)
        {
            db.Artistas.AddOrUpdate(artista);
            db.SaveChanges();

            Clients.All.update(artista);

            int removed;
            Locks.TryRemove(Context.ConnectionId, out removed);
            Clients.All.allLocks(Locks.Values);
        }
    }
}