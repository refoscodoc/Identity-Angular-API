using System.Net;
using MariaDb.API.Models;
using MariaDb.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MariaDb.API.Controllers;

[Authorize]
[Route("api/[controller]")]
public class ProductController : Controller
{
    private readonly BusinessProvider _business;

    public ProductController(BusinessProvider business)
    {
        _business = business;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductModel>), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _business.GetAllProducts());
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Get(string id)
    {
        var guid = Guid.Parse(id);
        return Ok(await _business.GetProduct(guid));
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ProductModel product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        if (product == null)
        {
            return BadRequest();
        }

        var result = await _business.AddProduct(product);

        return Created("/api/Product", result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Conflict)]
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null)
        {
            return BadRequest();
        }
            
        await _business.DeleteProduct(id);

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody]string productId, ProductModel product)
    {
        var productGuid = Guid.Parse(productId);
        return Ok(_business.UpdateProduct(productGuid, product));
    }
}