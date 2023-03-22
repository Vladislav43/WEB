using Fluentify.Database.Interfaces;
using Fluentify.Models.Database.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Fluentify.Database.Interfaces;
using Fluentify.Models.Database;
using System.Text.RegularExpressions;

namespace Fluentify.Database.Services
{
    public class UserService : IDbEntityService<Users>
    {
        private readonly StoreDbContext _dbContext;
        private bool _disposed;

        public UserService(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> UserNameExists(string userName)
        {
            return await _dbContext.Set<Users>().AnyAsync(x => x.Nickname == userName);
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _dbContext.Set<Users>().AnyAsync(x => x.Email == email);
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Users> Create(Users entity)
        {
            var entityFromOb = await _dbContext.Set<Users>().AddAsync(entity);
            await SaveChanges();

            return entityFromOb.Entity;
        }

        public async Task Delete(Users entity)
        {
            _dbContext.Set<Users>().Remove(entity);
            await SaveChanges();
        }

        public async Task<Users> GetById(int id)
        {
            return await _dbContext.Set<Users>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Users> Update(Users entity)
        {
            var entityFromOb = _dbContext.Set<Users>().Update(entity);
            await SaveChanges();

            return entityFromOb.Entity;
        }

        public IQueryable<Users> GetAll()
        {
            return _dbContext.Set<Users>();
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _dbContext.Dispose();
            _disposed = true;
        }
    }
}