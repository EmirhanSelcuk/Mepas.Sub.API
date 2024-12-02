using Mepas.Sub.API.Models;
using Mepas.Sub.API.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            if (!IsValidDateRange(KayitTarihiBaslangic, KayitTarihiBitis))
            {
                return new Result<List<Subscriber>> { IsSuccess = false, Message = "Kayıt tarih aralığı geçersiz. Başlangıç tarihi bitiş tarihinden sonra olamaz." };
            }

            if (!IsValidDateRange(AranmaTarihiBaslangic, AranmaTarihiBitis))
            {
                return new Result<List<Subscriber>> { IsSuccess = false, Message = "Arama tarih aralığı geçersiz. Başlangıç tarihi bitiş tarihinden sonra olamaz." };
            }

            // Tarihleri parse et
            DateTime? kayitBaslangic = string.IsNullOrEmpty(KayitTarihiBaslangic) ? null : DateTime.Parse(KayitTarihiBaslangic);
            DateTime? kayitBitis = string.IsNullOrEmpty(KayitTarihiBitis) ? null : DateTime.Parse(KayitTarihiBitis);
            DateTime? aranmaBaslangic = string.IsNullOrEmpty(AranmaTarihiBaslangic) ? null : DateTime.Parse(AranmaTarihiBaslangic);
            DateTime? aranmaBitis = string.IsNullOrEmpty(AranmaTarihiBitis) ? null : DateTime.Parse(AranmaTarihiBitis);

        
            var subscribers = await _subscriberRepository.GetSubscribersAsync(kayitBaslangic, kayitBitis, aranmaBaslangic, aranmaBitis);

            return new Result<List<Subscriber>> { IsSuccess = true, Data = subscribers };
        }

        // Tarih aralığının geçerli olup olmadığını kontrol et
        private bool IsValidDateRange(string startDate, string endDate)
        {
            if (string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
            {
                return true;
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

    // İşlem sonucu
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}