using System.Collections;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDb.API.Models;
using MongoDb.API.Services;
using Newtonsoft.Json;

namespace MongoDb.API.Controllers;

[Authorize]
[Route("[controller]")]
public class MongoDbController : Controller
{
    // private readonly HttpClient _httpClient;
    private readonly ILogger<MongoDbController> _logger;

    private readonly BusinessProvider _business;

    public MongoDbController(ILogger<MongoDbController> logger, BusinessProvider business)
    {
        _logger = logger;
        _business = business;
        // _httpClient = clientFactory.CreateClient("MongoDbApi");
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _business.GetAllTickers());
    }
    

    [HttpGet("{company}")]
    public async Task<IActionResult> Get(string? company)
    {
        // gets all but by company. hence I need to pass an identifier to the api
        
        // THIS GOES TO THE FRONTEND - IT CREATES A CLIENT FOR THE BACKEND
        // var response = await _httpClient.GetAsync("/ticker");
        // var content = await response.Content.ReadAsStringAsync();
        // var tickersList = JsonConvert.DeserializeObject<IEnumerable<TickerModel>>(content);

        // if (company is not null || company != string.Empty)
        // {
            return Ok(await _business.GetAllTickersByBrand(company));
        // }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody]TickerModel ticker)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        if (ticker == null)
        {
            return BadRequest();
        }
        
        var result = await _business.PostNewTicker(ticker);

        return Created("/MongoDb", result);
    }
}