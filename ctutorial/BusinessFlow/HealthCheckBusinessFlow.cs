using ctutorial.Models;
using Microsoft.EntityFrameworkCore;
using ctutorial.Data;
using System.Diagnostics;

namespace ctutorial.BusinessFlow
{
    public class HealthCheckBusinessFlow
    {
        private readonly MainContext _mainContext;
        public HealthCheckBusinessFlow(MainContext mainContext)
        {
            _mainContext = mainContext;
        }
        public HealthCheckResponse HealthCheckFlow()
        {
            HealthCheckEntity result = _mainContext.GetMasterContext.Set<HealthCheckEntity>().Where(x => x.id == 1).FirstOrDefault();
            return new HealthCheckResponse()
            {
                Status = result?.message,
                Version = Environment.GetEnvironmentVariable("API_VERSION")
            };
        }
        public string ProcessCreateTransaction(CreateTransactionRequest request)
        {
            Stopwatch watch = Stopwatch.StartNew();
            if (request != null)
            {
                UsersEntity userCreate = new UsersEntity()
                {
                    typeUser = request.typeUser,
                    userName = request.userName,
                    createdAt = DateTimeOffset.UtcNow,
                    updatedAt = DateTimeOffset.UtcNow
                };
                UsersEntity userSave = ProcessCreateUsers(userCreate);
                AccountingEntity accountingCreate = new AccountingEntity()
                {
                    amount = 100,
                    balance = 200,
                    userId = userSave.id,
                    outdatedBy = null,
                    createdAt = DateTimeOffset.UtcNow,
                    updatedAt = DateTimeOffset.UtcNow
                };
                ProcessCreateAccountingFirst(accountingCreate);
            }
            watch.Stop();
            long elapsedMs = watch.ElapsedMilliseconds;
            return $"Execution Time: {elapsedMs}ms";
        }
        private void ProcessCreateAccountingFirst(AccountingEntity accountingCreate)
        {
            AccountingEntity accountingFirst = Create(accountingCreate);
            AccountingEntity accountingCreateOutdate = new AccountingEntity()
            {
                amount = accountingFirst.balance,
                balance = 200 - accountingFirst.balance,
                userId = accountingFirst.userId,
                outdatedBy = null,
                createdAt = DateTimeOffset.UtcNow,
                updatedAt = DateTimeOffset.UtcNow
            };
            Create(accountingCreateOutdate);
            accountingFirst.outdatedBy = accountingCreateOutdate.id;
            Update(accountingFirst);
        }
        private UsersEntity ProcessCreateUsers(UsersEntity userCreate)
        {
            return Create(userCreate);
        }
        private void ResetContextState()
        {
            _mainContext.GetMasterContext.ChangeTracker.Entries()
            .Where(e => e.Entity != null).ToList()
            .ForEach(e => e.State = EntityState.Detached);
        }
        private T Create<T>(T entity) where T : class
        {
            try
            {
                _mainContext.GetMasterContext.Add(entity);
                _mainContext.GetMasterContext.SaveChanges();
            }
            catch
            {
                ResetContextState();
                throw;
            }
            return entity;
        }
        public T Update<T>(T entity) where T : class
        {
            try
            {
                _mainContext.GetMasterContext.Entry(entity).State = EntityState.Modified;
                _mainContext.GetMasterContext.SaveChanges();
            }
            catch
            {
                ResetContextState();
                throw;
            }
            return entity;
        }
    }
}
