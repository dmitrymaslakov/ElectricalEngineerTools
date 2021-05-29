using ElectricalEngineerTools.Framework.DAL.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectricalEngineerTools.Framework.DAL.Entities
{
    public class LightSourceInfo : BaseEntity
    {
        public LightSourceInfo()
        {
            LightingFixtures = new HashSet<LightingFixture>();
        }

        [Required]
        public string Power { get; set; }
        [Required]
        public string LightSourceType { get; set; }
        public string Socle { get; set; }
        public int? LampsNumber { get; set; }
        [NotMapped]
        public string FullDescription
        {
            get
            {
                return Socle == null 
                    ? $"{LightSourceType} {Power} Вт" 
                    : $"{LightSourceType} {Power} Вт цоколь {Socle} ({LampsNumber}шт.)";
            }
        }

        public virtual IEnumerable<LightingFixture> LightingFixtures { get; set; }
    }
}
