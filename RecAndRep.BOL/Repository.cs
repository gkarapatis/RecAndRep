using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecAndRep.DAL
{
    public class Repository<T> where T : IIdInterface
    {
        private string collectionName;
        public Repository(string name)
        {
            collectionName = name;
        }
        //private
        public void Add(T item)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<T>(collectionName);
                col.Insert(item);
            }

        }

        public void Update(T item)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<T>(collectionName);
                col.Update(item);
            }
        }

        public void Delete(int id)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<T>(collectionName);
                col.Delete(x => x.Id == id);
            }
        }

        public T GetById(int id)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<T>(collectionName);
                return col.FindOne(x => x.Id == id);
            }
        }

        public List<T> Get()
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<T>(collectionName);
                return col.FindAll().ToList();
            }
        }
    }
}
