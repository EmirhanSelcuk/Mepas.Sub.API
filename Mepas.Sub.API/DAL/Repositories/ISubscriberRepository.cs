using Mepas.Sub.API.Models;

public interface ISubscriberRepository
{
    Task CreateSubscriberAsync(Subscriber subscriber);
    Task<List<Subscriber>> GetSubscribersAsync(DateTime? kayitBaslangic, DateTime? kayitBitis, DateTime? aranmaBaslangic, DateTime? aranmaBitis);

}
