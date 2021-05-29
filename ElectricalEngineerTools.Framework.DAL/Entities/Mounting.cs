using ElectricalEngineerTools.Framework.DAL.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectricalEngineerTools.Framework.DAL.Entities
{
    public class Mounting : BaseEntity
    {
        public Mounting()
        {
            LightingFixtures = new HashSet<LightingFixture>();
        }

        [Required]
        public string MountingType { get; set; }
        [Required]
        public string MountingSubtype { get; set; }
        [NotMapped]
        public string FullDescription
        {
            get
            {
                return $"{MountingType} {MountingSubtype}";
            }
        }

        public virtual IEnumerable<LightingFixture> LightingFixtures { get; set; }
    }
}
