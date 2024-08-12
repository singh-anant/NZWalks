using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NZWalks.API.Model.Domain;

namespace NZWalks.API.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public double LengthInKM { get; set; }
        public string? WalkImageURL { get; set; }

        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        //here we are creating property.....
        //Navigation Properties=What difficulty a walk has
        public Difficulty difficulty { get; set; }

        //Walk will also have a region..
        public Region region { get; set; }
    }
}
