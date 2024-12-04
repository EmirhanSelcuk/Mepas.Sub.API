using Mepas.Sub.API.Helpers;
using Mepas.Sub.API.Models;

public interface ISubscriberService
{
    Task<Result<List<Subscriber>>> GetSubscribersAsync(DateTime KayitBaslangic, DateTime KayitBitis, DateTime AranmaBaslangic, DateTime AranmaBitis);
}
