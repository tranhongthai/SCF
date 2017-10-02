using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Peyton.Core.Common;
using Peyton.Core.Security;
using Peyton.Core.Messages;

namespace Peyton.Core.Repository
{
    public partial class DbContext
    {
        public string LanguageId { get; set; }
        //Security
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<User> Users { get; set; }
        //Enterprise

        public DbSet<Log> Logs { get; set; }


    }

    public partial class DbContext : System.Data.Entity.DbContext, IDisposable
    {
        public User GetCurrentUser()
        {
            return Load<User>(AppManager.CurrentUser.id);
        }
        void IDisposable.Dispose()
        {
            Dispose();
        }

        #region Constructors

        public DbContext() : base("Default")
        {
            LanguageId = "en";
        }

        public DbContext(string connection) : base(connection)
        {
        }

        #endregion

        #region Public Methods

        public T Load<T>(Expression<Func<T, bool>> ex, bool includeDeleted = true) where T : Entity
        {
            return Get<T>(includeDeleted).FirstOrDefault(ex);
        }

        public T Load<T>(Guid id, bool includeDeleted = true) where T : Entity
        {
            return Load<T>(i => i.oid == id, includeDeleted);
        }

        public T Load<T>(Guid id, string includePath , bool includeDeleted = false) where T : Entity
        {
            return Get<T>(i=>i.oid==id,includeDeleted).Include(includePath).FirstOrDefault();
        }

        public T Load<T>(string code, bool includeDeleted = true) where T : Entity
        {
            return Load<T>(i => i.Code == code);
        }

        public T Load<T>(long id, bool includeDeleted = false) where T : Entity
        {
            return Load<T>(i => i.id == id, includeDeleted);
        }

        public T Load<T>(long id, string includePath, bool includeDeleted = true) where T : Entity
        {
            return Get<T>(i=>i.id==id, includeDeleted).Include(includePath).FirstOrDefault();
        }


        public IQueryable<T> Get<T>( bool includeDeleted = false) where T : Data
        {
                return Set<T>().Where(i => i.Status != Status.Deleted, !includeDeleted);
        }
        public IQueryable<T> Get<T>(Expression<Func<T, bool>> ex,  bool includeDeleted = false) where T : Data
        {
            return Get<T>( includeDeleted).Where(ex);
        }

        public bool Exist<T>(Guid id) where T : Entity
        {
            return Set<T>().Any(i => i.oid == id);
        }

        public void Delete<T>(T data, bool isForced = false) where T : Data
        {
            if (data is Entity)
            {
                var entity = data as Entity;
                entity.ModifiedBy = AppManager.CurrentUser.id;
                entity.LastModifiedTime = DateTime.Now;
                entity.Status = Status.Deleted;
                if (isForced)
                    Entry(entity).State = EntityState.Deleted;
            }
            else
                Entry(data).State = EntityState.Deleted;
        }

        public void Add<T>(T data) where T : Data
        {
            if (data is Entity)
            {
                var entity = data as Entity;
                entity.CreatedBy = AppManager.CurrentUser.id;
                Set<T>().Add(entity as T);
            }
            else
                Set<T>().Add(data);
        }

        public void Update<T>(T entity) where T : Entity
        {
            entity.LastModifiedTime = DateTime.Now;
            
            entity.ModifiedBy = AppManager.CurrentUser.id;
        }

        public void ExecuteStoredProcedure(string name, Dictionary<string, object> paramaters)
        {
            Database.Connection.Open();
            var dbcommand = Database.Connection.CreateCommand();
            dbcommand.CommandType = CommandType.StoredProcedure;
            dbcommand.CommandText = name;

            foreach (var item in paramaters)
            {
                var parameter = dbcommand.CreateParameter();
                parameter.ParameterName = item.Key;
                parameter.Value = item.Value;
                dbcommand.Parameters.Add(parameter);
            }
            dbcommand.ExecuteNonQuery();
            Database.Connection.Close();
        }

        public void SaveChanges(ServiceResponse response)
        {
            try
            {
                if (!response.HasError)
                    SaveChanges();
            }
            catch (Exception ex)
            {
                response.AddError(ex);
            }
        }

        #endregion
    }

    public static class ContextExt
    {
        public static T Load<T>(this IQueryable<T> query) where T: Entity
        {
            var data = query.FirstOrDefault();
            if (data == null)
                data = (T)Activator.CreateInstance(typeof(T));
            return data;
        }
    }
}