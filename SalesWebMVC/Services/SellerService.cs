using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Models;
using SalesWebMVC.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Services
{
    public class SellerService
    {
        private readonly SalesWebMVCContext _context;

        public SellerService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller seller)
        {
            _context.Add(seller);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(seller => seller.Department).FirstOrDefaultAsync(seller => seller.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            var seller = await _context.Seller.FindAsync(id);
            _context.Remove(seller);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller sellerAtual)
        {
            var sellerCadastrado = await _context.Seller.AnyAsync(seller => seller.Id == sellerAtual.Id);
            if(!sellerCadastrado)
            {
                throw new NotFoundException("Seller was not encountered!");
            }
            try
            {
                _context.Seller.Update(sellerAtual);
                await _context.SaveChangesAsync();
            }

            /*Here, eventually if entity framework identifies a concurrency trouble conflict on db side
             *He will be register this trouble as DbUpdateConcurrencyException
             *With this "catch" configuration, we restriced this exception propagation only to the service layer,
             *this, on your time kickening our own exception called by DbConcurrencyException on service level
             *Maintaining the layers pattern integrity*/            
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
