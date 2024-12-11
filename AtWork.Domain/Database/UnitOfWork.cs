using AtWork.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace AtWork.Domain.Database
{
    public class UnitOfWork(DatabaseContext context, IServiceProvider serviceProvider) : IUnitOfWork
    {
        private IDbTransaction? transaction;

        public IDbTransaction BeginTransaction()
        {
            if (context.Database.GetDbConnection().State != ConnectionState.Open)
            {
                context.Database.OpenConnection();
            }

            transaction = context.Database.BeginTransaction().GetDbTransaction();
            return transaction;
        }

        public Exception? SaveChangesAsync()
        {
            try
            {
                context.SaveChanges();
                transaction?.Commit();
                return null;
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                return ex;
            }
        }

        public void Rollback()
        {
            transaction?.Rollback();
        }

        public TRepository Repository<TRepository>() where TRepository : class
        {
            var repository = serviceProvider.GetService<TRepository>();
            if (repository == null)
            {
                throw new InvalidOperationException($"O repositório {typeof(TRepository).Name} não está registrado no contêiner de DI.");
            }
            return repository;
        }

        public void Dispose()
        {
            transaction?.Dispose();
            context?.Dispose();
        }
    }
}
