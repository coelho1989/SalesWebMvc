using SalesWebMvc.Data;
using SalesWebMvc.Models;

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
            return _context.Seller.FirstOrDefault(s=>s.Id == id);
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
    }
}
