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

        //SO now we are going to make everything asynchronus so that MAIN Thread does not get ever blocked...
        //Get all the regions
        [HttpGet]
        public async Task<IActionResult> GetAllRegion()
        {
            //Region is the DbSet...
            var regionsDomain = await dBContext.Regions.ToListAsync();
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
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            var regionDomain = await dBContext.Regions.FindAsync(id);

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

        [HttpPost]
        public async Task<IActionResult> CreateRegion(
            [FromBody] AddRegionRequestDTO addRegionRequestDTO
        )
        {
            //Map or Convert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDTO.Code,
                Name = addRegionRequestDTO.Name,
                RegionImageURL = addRegionRequestDTO.RegionImageURL
            };
            //here we are saving it to the database..
            await dBContext.Regions.AddAsync(regionDomainModel);
            await dBContext.SaveChangesAsync();

            return Ok();
        }

        //Updating a Region...
        //PUT:https://localhost:1234/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateRegion(
            [FromRoute] Guid id,
            [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO
        )
        {
            //cheking the region if it exist or not
            var regionDomainModel = await dBContext.Regions.FindAsync(id);
            if (regionDomainModel == null)
                return NotFound();
            else
            {
                //start converitng
                //first start changing values of regionDomainModel
                regionDomainModel.Code = updateRegionRequestDTO.Code;
                regionDomainModel.Name = updateRegionRequestDTO.Name;
                regionDomainModel.RegionImageURL = updateRegionRequestDTO.RegionImageURL;
                //saving the changes
                await dBContext.SaveChangesAsync();

                //once again conveting it back..I dont know why
                var regionDTO = new RegionDTO();
                regionDTO.Code = regionDomainModel.Code;
                regionDTO.Name = regionDomainModel.Name;
                regionDTO.RegionImageURL = regionDomainModel.RegionImageURL;

                return Ok(regionDTO);
            }
        }

        //DELETE:https://localhost:1234/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
            var regionDomainModel = await dBContext.Regions.FindAsync(id);
            if (regionDomainModel == null)
                return NotFound();
            else
            {
                //Remove is a synchronous Function
                dBContext.Regions.Remove(regionDomainModel);
                await dBContext.SaveChangesAsync();
                return Ok();
            }
        }
    }
}
