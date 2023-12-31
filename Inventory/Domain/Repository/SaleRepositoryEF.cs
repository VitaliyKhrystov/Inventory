﻿using Inventory.Domain.Entities;
using Inventory.Domain.Repository.Abstract;
using Inventory.Pages;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Domain.Repository
{
    public class SaleRepositoryEF : ISaleRepository
    {
        private readonly AppDbContext context;

        public SaleRepositoryEF(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Guid> Create(SalesEntity sale)
        {
            await context.Sales.AddAsync(sale);
            await context.SaveChangesAsync();
            return sale.Id;
        }

        public async Task Delete(SalesEntity sale)
        {
            context.Sales.Remove(sale);
            await context.SaveChangesAsync();
        }

        public async Task DeleteRange(List<SalesEntity> sales)
        {
            context.Sales.RemoveRange(sales);
            await context.SaveChangesAsync();
        }

        public async Task<List<SalesEntity>> GetAll()
        {
            return await context.Sales.Include(c => c.Customer).ToListAsync();
        }

        public async Task<SalesEntity> GetById(Guid id)
        {
            return await context.Sales.Include(s =>s.SalesSummaries).Include(s => s.Customer).Include(s => s.SalesVariants).ThenInclude(p=>p.Product).ThenInclude(p=>p.Variants).FirstOrDefaultAsync(c => c.Id == id, default);
        }

        public async Task Update(SalesEntity sale)
        {
            context.Sales.Update(sale);
            await context.SaveChangesAsync();
        }

        public int GetLastVoucherIdByDate(DateOnly date)
        {
            var order = context.Sales.OrderByDescending(p => p.VoucherId).FirstOrDefault(s => s.Date == date);
            if (order != null)
            {
                return order.VoucherId;
            }
            return 0;
        }

        public bool IsVoucherExistByDate(int voucherId, DateOnly date)
        {
            return context.Sales.Any(p => p.VoucherId == voucherId && p.Date == date);
        }
    }
}
