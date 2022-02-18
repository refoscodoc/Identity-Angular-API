using System.Collections;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using MongoDb.API.Models;
using MongoDb.API.Services;
using Newtonsoft.Json;

namespace MongoDb.API.Controllers;

[Route("[controller]")]
public class MongoDbController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<MongoDbController> _logger;

    private readonly BusinessProvider _business;

    public MongoDbController(IHttpClientFactory clientFactory, ILogger<MongoDbController> logger, BusinessProvider business)
    {
        _logger = logger;
        _business = business;
        _httpClient = clientFactory.CreateClient("MongoDbApi");
    }

    [HttpGet("{company}")]
    public async Task<IActionResult> Get(string company)
    {
        // gets all but by company. hence I need to pass an identifier to the api
        // var response = await _httpClient.GetAsync("/ticker");
        // var content = await response.Content.ReadAsStringAsync();
        // var tickersList = JsonConvert.DeserializeObject<IEnumerable<TickerModel>>(content);

        return Ok(await _business.GetAllTickers(company));
    }
}