using EFExample.Domain.Entities;
using EFExample.Domain.Enums;
using EFExample.Repository.EFRepository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EFExample.Repository.EFRepository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase, new()
    {
        private readonly ApplicationDbContext _context;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            var dbSet = _context.Set<TEntity>();
            dbSet.AddRange(entities);
            _context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (includeProperties != null)
            {
                query = ApplyIncludesOnQuery(query, includeProperties);
            }
            return query.FirstOrDefault(predicate);
        }

        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Where(x => x.Id.Equals(id)).FirstOrDefault();
        }

        public TEntity GetById(int id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            query = query.Where(x => x.Id.Equals(id));
            if (includeProperties != null)
            {
                query = ApplyIncludesOnQuery(query, includeProperties);
            }

            return query.FirstOrDefault();
        }


        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            return predicate == null ? _context.Set<TEntity>().ToList() : _context.Set<TEntity>().Where(predicate).ToList();
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (includeProperties != null)
            {
                query = ApplyIncludesOnQuery(query, includeProperties);
            }
            return predicate == null ? query.ToList() : query.Where(predicate).ToList();
        }


        public void Insert(TEntity entity)
        {
            var dbSet = _context.Set<TEntity>();
            dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            _context.SaveChanges();
        }

        public void SoftDelete(TEntity entity)
        {
            EntityBase entityBase = entity;
            entityBase.Statu = StatusType.Deleted;
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IQueryable<TEntity> ApplyIncludesOnQuery<TEntity>(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : EntityBase, new()
        {
            // Return Applied Includes query
            return (includeProperties.Aggregate(query, (current, include) => current.Include(include)));
        }
    }
}
