using Mepas.Sub.API.Helpers;
using Mepas.Sub.API.Models;
using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
public class SubscriberController : ControllerBase
{
    private readonly ISubscriberService _subscriberService;



    public SubscriberController(ISubscriberService subscriberService)
    {
        _subscriberService = subscriberService;
    }

    [HttpPost]
    public async Task<ActionResult<Subscriber>> CreateSubscriber([FromBody] Subscriber subscriber)
    {
        if (subscriber == null)
        {
            return BadRequest("Abone bilgileri eksik.");
        }

        var result = await _subscriberService.CreateSubscriberAsync(subscriber);

        if (!result.IsSuccess)
        {
            return StatusCode(500, result.Message);
        }

        return CreatedAtAction(nameof(GetSubscribers), new { id = subscriber.Id }, subscriber);
    }

    [HttpGet]
    public async Task<ActionResult<List<Subscriber>>> GetSubscribers(string KayitTarihiBaslangic, string KayitTarihiBitis, string AranmaTarihiBaslangic, string AranmaTarihiBitis)
    {
        // Tarihleri parse et
        DateTime kayitBaslangic = DateTime.Parse(KayitTarihiBaslangic);
        DateTime kayitBitis = DateTime.Parse(KayitTarihiBitis);
        DateTime aranmaBaslangic = DateTime.Parse(AranmaTarihiBaslangic);
        DateTime aranmaBitis = DateTime.Parse(AranmaTarihiBitis);

        // Kayıt tarih aralığını doğrulama
        if (!DateHelper.IsValidDateRange(kayitBaslangic, kayitBitis))
        {
            throw new ArgumentException("Kayıt başlangıç tarihi geçersiz.");
        }

        // Aranma tarih aralığını doğrulama
        if (!DateHelper.IsValidDateRange(aranmaBaslangic, aranmaBitis))
        {
            throw new ArgumentException("Aranma bitiş tarihi geçersiz.");
        }

        // SubscriberService'e veri gönder
        var result = await _subscriberService.GetSubscribersAsync(kayitBaslangic, kayitBitis, aranmaBaslangic, aranmaBitis);
        return Ok(result.Data);
    }
}
