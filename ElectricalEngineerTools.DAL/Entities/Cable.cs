using ElectricalEngineerTools.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectricalEngineerTools.DAL.Entities
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
        public IEnumerable<LightingFixture> LightingFixtures { get; set; }
    }
}
