using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElectricalEngineerTools.Framework.DAL.Entities
{
    public class Manufacturer : BaseEntity
    {
        public Manufacturer()
        {
            LightingFixtures = new HashSet<LightingFixture>();
        }
        [Required]
        public string Name { get; set; }
        public virtual IEnumerable<LightingFixture> LightingFixtures { get; set; }
    }
}
