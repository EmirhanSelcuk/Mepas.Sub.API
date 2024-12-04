using Mepas.Sub.API.Helpers;
using Mepas.Sub.API.Models;
using Mepas.Sub.API.Repositories;

namespace Mepas.Sub.API.Services
{
    public interface ISubscriberService
    {
        Task<Result<List<Subscriber>>> GetSubscribersAsync(string KayitTarihiBaslangic, string KayitTarihiBitis, string AranmaTarihiBaslangic, string AranmaTarihiBitis);
    }

    public class SubscriberService : ISubscriberService
    {
        private readonly ISubscriberRepository _subscriberRepository;

        public SubscriberService(ISubscriberRepository subscriberRepository)
        {
            _subscriberRepository = subscriberRepository;
        }

        public async Task<Result<List<Subscriber>>> GetSubscribersAsync(
            string KayitTarihiBaslangic,
            string KayitTarihiBitis,
            string AranmaTarihiBaslangic,
            string AranmaTarihiBitis)
        {
            // Tarihleri parse et
            var (kayitBaslangic, kayitBitis) = DateHelper.ParseDateRange(KayitTarihiBaslangic, KayitTarihiBitis);
            var (aranmaBaslangic, aranmaBitis) = DateHelper.ParseDateRange(AranmaTarihiBaslangic, AranmaTarihiBitis);

            // Kayıt tarih aralığını doğrulama
            if (!DateHelper.IsValidDateRange(kayitBaslangic, kayitBitis))
            {
                return new Result<List<Subscriber>> { IsSuccess = false, Message = "Kayıt tarih aralığı geçersiz. Başlangıç tarihi bitiş tarihinden sonra olamaz." };
            }

            // Aranma tarih aralığını doğrulama
            if (!DateHelper.IsValidDateRange(aranmaBaslangic, aranmaBitis))
            {
                return new Result<List<Subscriber>> { IsSuccess = false, Message = "Arama tarih aralığı geçersiz. Başlangıç tarihi bitiş tarihinden sonra olamaz." };
            }

            // Veritabanından aboneleri al
            var subscribers = await _subscriberRepository.GetSubscribersAsync(kayitBaslangic, kayitBitis, aranmaBaslangic, aranmaBitis);

            return new Result<List<Subscriber>> { IsSuccess = true, Data = subscribers };
        }
    }
}
