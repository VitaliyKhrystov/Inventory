﻿using Inventory.Domain.Entities;
using Inventory.Domain.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Domain.Repository
{
    public class VariantRepositoryEF : IVariantRepository
    {
        private readonly AppDbContext context;

        public VariantRepositoryEF(AppDbContext context)
        {
            this.context = context;
        }
        public async Task Create(VariantEntity variant)
        {
            await context.Variants.AddAsync(variant);
            await context.SaveChangesAsync();
        }

        public async Task Delete(VariantEntity variant)
        {
            context.Variants.Remove(variant);
            await context.SaveChangesAsync();
        }

        public async Task<List<VariantEntity>> GetAll()
        {
            return await context.Variants.AsNoTracking().ToListAsync();
        }

        public async Task<VariantEntity> GetById(Guid id)
        {
            return await context.Variants.FirstOrDefaultAsync(c => c.Id == id, default);
        }

        public async Task Update(VariantEntity variant)
        {
            context.Variants.Update(variant);
            await context.SaveChangesAsync();
        }
    }
}