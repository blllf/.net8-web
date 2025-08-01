using Blf.Net8.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blf.Net8.EntityFramework.Repositories {
    /*where T : class
    这是 泛型约束（Generic Constraint）
    含义：T 必须是一个引用类型（如类、接口、委托等），不能是值类型（如 int、DateTime、struct）*/
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class {
        protected GameManagermentDbContext _dbContext { get;}
        public RepositoryBase(GameManagermentDbContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task Create(T entity) {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity) {
            _dbContext.Set<T>().Remove(entity);
        }
        // AsNoTracking(): 设置为不跟踪状态，提高查询性能（适用于只读场景）
        // 返回的是 IQueryable<T>，它不是数据，而是一个查询的描述（包含要查什么表、什么条件、是否排序等）。
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) {
            return _dbContext.Set<T>().Where(expression).AsNoTracking();
        }

        public IQueryable<T> GetAll() {
            return _dbContext.Set<T>().AsNoTracking();
        }

        public void Update(T entity) {
            _dbContext.Set<T>().Update(entity);
        }

        public Task<int> SaveChangesAsync() {
            return _dbContext.SaveChangesAsync(); // 非阻塞，释放线程
        }

    }
}
