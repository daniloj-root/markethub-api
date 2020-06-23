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

    public class TradesController : ControllerBase
    {
        private TradesRepository _tradesRepository { get; set; }

        public TradesController()
        {
            _tradesRepository = new TradesRepository();
        }

        [HttpGet]
        public IActionResult GetLastTrade()
        {
            try
            {
                return Ok(_tradesRepository.GetLastTrade());
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Container is empty or doesn't exist");
            }
        }

        [HttpGet("/{depth}")]
        public IActionResult GetLastTrades(int depth)
        {
            try
            {
                return Ok(_tradesRepository.GetLastTrades(depth));
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Container doesn't have enough items, is empty or doesn't exist");
            }
        }

        [HttpGet("/{stock}")]
        public IActionResult GetLastTradeByStock(string stock)
        {
            try
            {
                return Ok(_tradesRepository.GetLastTradeByStock(stock));
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Your stock was not found, container is empty or doesn't exist");
            }
        }

        [HttpGet("/book/{depth}")]
        public IActionResult GetOrderBook(int depth)
        {
            try
            {
                return Ok(_tradesRepository.GetOrderBook(depth));
            }
            catch (Exception)
            {
                return NotFound("Container doesn't have enough items, is empty or doesn't exist");
            }
        }
    }
}

