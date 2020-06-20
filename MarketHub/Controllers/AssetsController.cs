using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MarketHub.Libraries;
using MarketHub.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketHub.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]

    public class AssetsController : ControllerBase
    {
        private AssetRepository _assetRepository { get; set; }

        [HttpGet("/{container}/{id}")]
        public IActionResult GetLast(string container, int id)
        {
            try
            {
                var _assetRepository = new AssetRepository(container);

                return Ok(_assetRepository.GetLast());
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}

