using Mepas.Sub.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mepas.Sub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriberController : Controller
    {
        private readonly AppDbContext _context;

        public SubscriberController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subscriber>>> GetSubscribers(
            [FromQuery] string KayitTarihiBaslangic,
            [FromQuery] string KayitTarihiBitis,
            [FromQuery] string AranmaTarihiBaslangic,
            [FromQuery] string AranmaTarihiBitis)
        {
            // Tarihlerin geçerli olup olmadığını kontrol et
            if (!IsValidDateRange(KayitTarihiBaslangic, KayitTarihiBitis))
            {
                return BadRequest("Kayıt tarih aralığı geçersiz. Başlangıç tarihi bitiş tarihinden sonra olamaz.");
            }

            if (!IsValidDateRange(AranmaTarihiBaslangic, AranmaTarihiBitis))
            {
                return BadRequest("Arama tarih aralığı geçersiz. Başlangıç tarihi bitiş tarihinden sonra olamaz.");
            }

            // Tarihleri kontrol et ve DateTime türüne çevir
            DateTime? kayitBaslangic = string.IsNullOrEmpty(KayitTarihiBaslangic) ? null : DateTime.Parse(KayitTarihiBaslangic);
            DateTime? kayitBitis = string.IsNullOrEmpty(KayitTarihiBitis) ? null : DateTime.Parse(KayitTarihiBitis);
            DateTime? aranmaBaslangic = string.IsNullOrEmpty(AranmaTarihiBaslangic) ? null : DateTime.Parse(AranmaTarihiBaslangic);
            DateTime? aranmaBitis = string.IsNullOrEmpty(AranmaTarihiBitis) ? null : DateTime.Parse(AranmaTarihiBitis);

            var query = _context.Subscribers.AsQueryable();

            // Kayıt tarih aralığını filtrele
            if (kayitBaslangic.HasValue && kayitBitis.HasValue)
                query = query.Where(r => r.KayitTarihi >= kayitBaslangic.Value && r.KayitTarihi <= kayitBitis.Value);

            // Aranma tarih aralığını filtrele
            if (aranmaBaslangic.HasValue && aranmaBitis.HasValue)
                query = query.Where(r => r.AranmaTarihi >= aranmaBaslangic.Value && r.AranmaTarihi <= aranmaBitis.Value);

            var subscribers = await query.ToListAsync();
            return Ok(subscribers);
        }

        // Geçerli tarih aralığını kontrol et
        private bool IsValidDateRange(string startDate, string endDate)
        {
            if (string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
            {
                return true; // Eğer tarih parametrelerinden biri boşsa, geçerli kabul et
            }

            DateTime start;
            DateTime end;

            if (!DateTime.TryParse(startDate, out start) || !DateTime.TryParse(endDate, out end))
            {
                return false; // Tarih geçersiz formatta
            }

            return start <= end; // Başlangıç tarihi, bitiş tarihinden önce olmalıdır
        }
    }
}