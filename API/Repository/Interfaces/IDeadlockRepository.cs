using API.Models;
using System.Collections.Generic;

namespace API.Repository.Interfaces
{
    public interface IDeadlockRepository
    {
        ICollection<Deadlock> GetDeadlocks();
        Deadlock GetDeadlock(string id);
        bool DoesDeadlockExist(string id);
        string AddDeadlock(Deadlock deadlock);
        void UpdateDeadlock(Deadlock deadlock);
        void DeleteDeadlock(string id);
    }
}
