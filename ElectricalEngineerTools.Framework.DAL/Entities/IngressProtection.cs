using ElectricalEngineerTools.Framework.DAL.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectricalEngineerTools.Framework.DAL.Entities
{
    public class IngressProtection : BaseEntity
    {
        public IngressProtection()
        {
            LightingFixtures = new HashSet<LightingFixture>();
        }

        public int Value { get; set; }
        public virtual IEnumerable<LightingFixture> LightingFixtures { get; set; }
    }
}
