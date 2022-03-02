using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDb.API.Models;

namespace GoogleSheetsAPI.Controllers;

[Authorize]
[Route("[controller]")]
public class GoogleSheetsController : Controller
{
    private readonly GoogleSheets _sheets;

    public GoogleSheetsController(GoogleSheets sheets)
    {
        _sheets = sheets;
    }

    [HttpGet]
    public async Task<IActionResult> MongoToSheets()
    {
        var result = await _sheets.FetchAllFromMongo();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> SheetsToMongo()
    {
        var result = await _sheets.PopulateFromXls();
        return Ok(result);
    }
}