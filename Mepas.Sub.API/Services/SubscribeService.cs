using Mepas.Sub.API.Helpers;
using Mepas.Sub.API.Models;
using Mepas.Sub.API.Repositories;

public class SubscriberService : ISubscriberService
{
    private readonly ISubscriberRepository _subscriberRepository;

    public SubscriberService(ISubscriberRepository subscriberRepository)
    {
        _subscriberRepository = subscriberRepository;
    }

    public async Task<Result<List<Subscriber>>> GetSubscribersAsync(DateTime KayitBaslangic, DateTime KayitBitis, DateTime AranmaBaslangic, DateTime AranmaBitis)
    {
        // Veritabanından aboneleri al
        var subscribers = await _subscriberRepository.GetSubscribersAsync(KayitBaslangic, KayitBitis, AranmaBaslangic, AranmaBitis);
        return new Result<List<Subscriber>> { IsSuccess = true, Data = subscribers };
    }
}
