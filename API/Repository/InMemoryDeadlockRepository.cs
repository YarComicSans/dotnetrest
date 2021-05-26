using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using API.Models;
using API.Repository.Interfaces;

namespace API.Repository
{
    public class InMemoryDeadlockRepository : IDeadlockRepository
    {
        private readonly Dictionary<string, Deadlock> _deadlocks;
        public InMemoryDeadlockRepository()
        {
            _deadlocks = new Dictionary<string, Deadlock>();
        }
        public ICollection<Deadlock> GetDeadlocks()
        {
            Deadlock[] deadlocks = new Deadlock[_deadlocks.Count];
            _deadlocks.Values.CopyTo(deadlocks, 0);

            return deadlocks;
        }

        public Deadlock GetDeadlock(string id)
        {
            if(!DoesDeadlockExist(id)) return null;
            var deadlock = _deadlocks[id];
            return deadlock;
        }

        public bool DoesDeadlockExist(string id)
        {
            return _deadlocks.ContainsKey(id);
        }

        public string AddDeadlock(Deadlock deadlock)
        {
            deadlock.Id = CreateId(deadlock.ProcessId, deadlock.CreatedDate);

            var deadlockExists = DoesDeadlockExist(deadlock.Id);
            if(deadlockExists) return null;

            _deadlocks.Add(deadlock.Id, deadlock);
            return deadlock.Id;
        }

        public void UpdateDeadlock(Deadlock deadlock)
        {
            _deadlocks[deadlock.Id] = deadlock;
        }

        public void DeleteDeadlock(string id)
        {
            _deadlocks.Remove(id);
        }

        private string CreateId(string processId, DateTime? createdDate)
        {
            var concatenatedFieldData = string.Join(processId, createdDate == null ? createdDate.ToString() : "");
            
            using var hashAlgorithm = SHA256.Create();
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(concatenatedFieldData));

            var sBuilder = new StringBuilder();
            for(int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            var hash = sBuilder.ToString();

            return hash;
        }
    }
}
