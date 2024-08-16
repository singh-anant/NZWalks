using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Controller
{
    //https://localhost:1234/api/regions
    [ApiController]
    [Route("api/[controller]")]
    //The problem with this function are that we are exposing every single data to the client
    //But for explae we have a password or something like that it can cause security issues
    //So thats why we need DTO'S.
    //DTO's will contain that data that we want to expose to the client....
    //DTO can be subset of domain model...
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDBContext dBContext;

        public RegionsController(NZWalksDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        //Get all the regions
        [HttpGet]
        public IActionResult GetAll()
        {
            //Region is the DbSet...
            var regions = dBContext.Regions.ToList();
            //System.Console.WriteLine(regions);
            //if we want to talk to the database...
            return Ok(regions);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetRegionById(Guid id)
        {
            var region = dBContext.Regions.Find(id);

            if (region == null)
                return NotFound();
            else
                return Ok(region);
        }
    }
}
