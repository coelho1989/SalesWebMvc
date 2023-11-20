using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;
using DbUpdateConcurrencyException = SalesWebMvc.Services.Exceptions.DbUpdateConcurrencyException;

namespace SalesWebMvc.Services
{
    public class SellerService
    {

        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
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
            return _context.Seller.Include(s=>s.Department).FirstOrDefault(s=>s.Id == id);
        }

        public void Remove(int id)
        {
            Seller seller = _context.Seller.Find(id);
            if (seller != null)
            {
                _context.Seller.Remove(seller);
                _context.SaveChanges();
            }
        }

        public void Update(Seller seller)
        {
            if (!_context.Seller.Any(x=>x.Id == seller.Id)) {
                throw new NotFoundException("Id not found");
            }

            try
            {

                _context.Update(seller);
                _context.SaveChanges();
            } catch (Exceptions.DbUpdateConcurrencyException ex) { 
                throw new DbUpdateConcurrencyException(ex.Message);
            }
        }
    }
}
