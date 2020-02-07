using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoPOA.Models;

namespace ProyectoPOA.Repositories
{
    public abstract class Repository<T> where T : class
    {
        public itesrcne_poaContext Context { get; set; }

        public Repository()
        {
            Context = new itesrcne_poaContext();
        }

        public Repository(itesrcne_poaContext context)
        {
            Context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>();
        }

        public T GetById(int id)
        {
            return Context.Find<T>(id);
        }

        public void Insert(T entidad)
        {
            Context.Add(entidad);
            Save();
        }

        public void Update(T entidad)
        {
            Context.Update(entidad);
            Save();
        }

        public void Delete(T enitdad)
        {
            Context.Remove(enitdad);
            Save();
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
