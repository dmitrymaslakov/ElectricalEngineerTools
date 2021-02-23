using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElectricalEngineerTools.Framework.DAL.Entities
{
    public class Cable : BaseEntity
    {
        public Cable()
        {
            LightingFixtures = new HashSet<LightingFixture>();
        }
        [Required]
        public string Brand { get; set; }
        [Required]
        public int CoresNumber { get; set; }
        [Required]
        public double Section { get; set; }
        public virtual IEnumerable<LightingFixture> LightingFixtures { get; set; }
    }
}
