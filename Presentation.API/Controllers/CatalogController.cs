using Domain.Core.DTOs;
using Domain.Core.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Catalog>>> Get()
        {
            var catalogs = await _catalogService.GetCatalogsAsync();
            return Ok(catalogs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Catalog>> Get(string id)
        {
            var catalog = await _catalogService.GetCatalogAsync(id);
            if (catalog == null)
            {
                return NotFound();
            }
            return Ok(catalog);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CatalogRequest catalogRequest)
        {
            await _catalogService.CreateCatalogAsync(catalogRequest);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] CatalogRequest catalogRequest)
        {
            try
            {
                await _catalogService.UpdateCatalogAsync(id, catalogRequest);
            }
            catch (Exception)
            {
                return NotFound();
            }
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _catalogService.DeleteCatalogAsync(id);
            return NoContent();
        }
    }
}
