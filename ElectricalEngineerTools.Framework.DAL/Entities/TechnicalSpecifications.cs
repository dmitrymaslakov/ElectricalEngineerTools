using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElectricalEngineerTools.Framework.DAL.Entities
{
    public class TechnicalSpecifications : BaseEntity
    {
        public TechnicalSpecifications()
        {
            LightingFixtures = new HashSet<LightingFixture>();
        }
        [Required]
        public string Number { get; set; }
        public virtual IEnumerable<LightingFixture> LightingFixtures { get; set; }
    }
}
