using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Peyton.Core.Common;
using Peyton.Core.Report;
using Peyton.Core.Security;

namespace Peyton.Core.Repository
{
    public partial class DbContext
    {
        public string LanguageId { get; set; }
        //Security
        public DbSet<Profile> Profiles { get; set; }
        //Enterprise
        public DbSet<Common.System> Systems { get; set; }

        public DbSet<ImportanceLevel> ImportanceLevels { get; set; }
        public DbSet<GroupLevel> GroupLevels { get; set; }
        public DbSet<GroupType> GroupTypes { get; set; }
        public DbSet<ErrorMessage> ErrorMessages { get; set; }
        public DbSet<LogFormat> LogFormats { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<ResultCode> ResultCodes { get; set; }
        public DbSet<RoleType> RoleTypes { get; set; }
        public DbSet<UserRoleType> UserRoleTypes { get; set; }
        public DbSet<CreditCardType> CreditCardTypes { get; set; }
        public DbSet<Milestone> Milestones { get; set; }

        public DbSet<ServiceReport> ServiceReports { get; set; }

        public DbSet<Log> Logs { get; set; }

    }

    public partial class DbContext : System.Data.Entity.DbContext, IDisposable
    {
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

        public IQueryable<T> Get<T>(Expression<Func<T, bool>> ex) where T : Entity
        {
            return Set<T>().Where(ex);
        }

        public T Load<T>(Expression<Func<T, bool>> ex) where T : Entity
        {
            return Set<T>().FirstOrDefault(ex);
        }

        public T Get<T>(Guid id) where T : Entity
        {
            return Set<T>().FirstOrDefault(i => i.Id == id);
        }

        public T Get<T>(int id) where T : EnumEntity
        {
            return Set<T>().FirstOrDefault(i => i.Id == id);
        }

        public T Get<T>(string name) where T : EnumEntity
        {
            return Set<T>().FirstOrDefault(i => i.Name == name);
        }

        public bool Exist<T>(Guid id) where T : Entity
        {
            return Set<T>().Any(i => i.Id == id);
        }


        public IQueryable<T> Get<T>() where T : Entity
        {
            return Set<T>().AsQueryable();
        }

        public void Delete<T>(T entity) where T : Entity
        {
            Entry(entity).State = EntityState.Deleted;
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

        #endregion
    }
}