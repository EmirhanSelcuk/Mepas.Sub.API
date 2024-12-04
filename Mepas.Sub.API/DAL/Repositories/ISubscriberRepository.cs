using Mepas.Sub.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Mepas.Sub.API.Repositories
{
    public interface ISubscriberRepository
    {
        Task<List<Subscriber>> GetSubscribersAsync(DateTime? kayitBaslangic, DateTime? kayitBitis, DateTime? aranmaBaslangic, DateTime? aranmaBitis);
    }

    public class SubscriberRepository : ISubscriberRepository
    {
        private readonly AppDbContext _context;

        public SubscriberRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Subscriber>> GetSubscribersAsync(
            DateTime? kayitBaslangic,
            DateTime? kayitBitis,
            DateTime? aranmaBaslangic,
            DateTime? aranmaBitis)
        {
            var query = _context.Subscribers.AsQueryable();

            if (kayitBaslangic.HasValue && kayitBitis.HasValue)
                query = query.Where(r => r.KayitTarihi >= kayitBaslangic.Value && r.KayitTarihi <= kayitBitis.Value);

            if (aranmaBaslangic.HasValue && aranmaBitis.HasValue)
                query = query.Where(r => r.AranmaTarihi >= aranmaBaslangic.Value && r.AranmaTarihi <= aranmaBitis.Value);

            return await query.ToListAsync();
        }
    }
}
