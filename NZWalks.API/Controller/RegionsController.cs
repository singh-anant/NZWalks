using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

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
        public IActionResult GetAllRegion()
        {
            //Region is the DbSet...
            var regionsDomain = dBContext.Regions.ToList();
            //System.Console.WriteLine(regions);
            //if we want to talk to the database...

            //WE NEED TO MAP DOMAIN TO DTO
            var regionsDTO = new List<RegionDTO>();

            foreach (var region in regionsDomain)
            {
                // regionsDTO.Add(
                //     new RegionDTO()
                //     {
                //         Id = region.Id,
                //         Name = region.Name,
                //         Code = region.Code,
                //         RegionImageURL = region.RegionImageURL
                //     }
                // );
                RegionDTO regionsDTOS = new RegionDTO();
                regionsDTOS.Id = region.Id;
                regionsDTOS.Name = region.Name;
                regionsDTOS.Code = region.Code;
                regionsDTOS.RegionImageURL = region.RegionImageURL;

                regionsDTO.Add(regionsDTOS);
            }
            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetRegionById(Guid id)
        {
            var regionDomain = dBContext.Regions.Find(id);

            if (regionDomain == null)
                return NotFound();
            else
            {
                RegionDTO regionDTO = new RegionDTO()
                {
                    Id = regionDomain.Id,
                    Name = regionDomain.Name,
                    Code = regionDomain.Code,
                    RegionImageURL = regionDomain.RegionImageURL
                };
                return Ok(regionDTO);
            }
        }
    }
}
