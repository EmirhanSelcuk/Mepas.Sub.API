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
            return BadRequest("Kayıt tarih aralığı geçersiz.");
        }

        // Aranma tarih aralığını doğrulama
        if (!DateHelper.IsValidDateRange(aranmaBaslangic, aranmaBitis))
        {
            return BadRequest("Arama tarih aralığı geçersiz.");
        }

        // SubscriberService'e veri gönder
        var result = await _subscriberService.GetSubscribersAsync(kayitBaslangic, kayitBitis, aranmaBaslangic, aranmaBitis);
        return Ok(result.Data);
    }
}
