using ElectricalEngineerTools.Framework.DAL.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectricalEngineerTools.Framework.DAL.Entities
{
    public class EquipmentClass : BaseEntity
    {
        public EquipmentClass()
        {
            LightingFixtures = new HashSet<LightingFixture>();
        }
        [Required]
        public string Value { get; set; }
        public virtual IEnumerable<LightingFixture> LightingFixtures { get; set; }
    }
}
