using Mepas.Sub.API.Helpers;
using Mepas.Sub.API.Models;

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

    public async Task<Result<Subscriber>> CreateSubscriberAsync(Subscriber subscriber)
    {
        try
        {
            await _subscriberRepository.CreateSubscriberAsync(subscriber); // Veritabanına ekleme işlemi
            return new Result<Subscriber> { Data = subscriber, IsSuccess = true };
        }
        catch (Exception ex)
        {
            return new Result<Subscriber> { IsSuccess = false, Message = ex.Message };
        }
    }
}
