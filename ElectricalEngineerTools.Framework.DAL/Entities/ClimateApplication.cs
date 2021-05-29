using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalEngineerTools.Framework.DAL.Entities
{
    public class ClimateApplication : BaseEntity
    {
        public ClimateApplication()
        {
            LightingFixtures = new HashSet<LightingFixture>();
        }

        [Required]
        public string Value { get; set; }
        public virtual IEnumerable<LightingFixture> LightingFixtures { get; set; }
    }
}
