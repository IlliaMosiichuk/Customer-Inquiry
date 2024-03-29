﻿using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _dbContext;

        protected DbSet<T> _dbSet
        {
            get
            {
                return _dbContext.Set<T>();
            }
        }

        public EfRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public virtual IEnumerable<T> GetAll(Func<T, bool> predicate = null)
        {
            if (predicate == null)
            {
                return _dbSet.ToList();
            }

            return _dbSet.Where(predicate).ToList();
        }

        public virtual T GetById(long id)
        {
            return _dbSet.Find(id);
        }

    }
}
