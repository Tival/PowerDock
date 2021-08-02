using LiteDB;
using PowerSideDock.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PowerSideDock.Database {
    public abstract class GenericRepository<T> where T : Dbo {
        private readonly ILiteCollection<T> collection;

        public GenericRepository(DbContext context) {
            this.collection = context.database.GetCollection<T>();
        }

        public void Create(T entity) {
            collection.Insert(entity);
        }

        public List<T> Find(Expression<Func<T, bool>> expression, int? skip = null, int? take = null) {
            var query = collection.Query().Where(expression);

            if (skip != null && take != null) {
                return query.Skip(skip.Value).Limit(take.Value).ToList();
            }

            return query.ToList();
        }

        public bool Remove(T entity) {
            return collection.Delete(new BsonValue(entity.Id));
        }

        public bool Remove(int id) {
            return collection.Delete(new BsonValue(id));
        }

        public bool Update(T entity) {
            return collection.Update(entity);
        }

        public int Update(IEnumerable<T> entities) {
            return collection.Update(entities);
        }
    }
}
