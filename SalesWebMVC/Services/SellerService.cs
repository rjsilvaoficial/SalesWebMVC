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

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller seller)
        {
            _context.Add(seller);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            return _context.Seller.Include(seller => seller.Department).FirstOrDefault(seller => seller.Id == id);
        }

        public void Remove(int id)
        {
            var seller = _context.Seller.Find(id);
            _context.Remove(seller);
            _context.SaveChanges();
        }

        public void Update(Seller sellerAtual)
        {
            if(!_context.Seller.Any(sellerAnterior => sellerAnterior.Id == sellerAtual.Id))
            {
                throw new NotFoundException("Seller was not encountered!");
            }
            try
            {
                _context.Seller.Update(sellerAtual);
                _context.SaveChanges();
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
