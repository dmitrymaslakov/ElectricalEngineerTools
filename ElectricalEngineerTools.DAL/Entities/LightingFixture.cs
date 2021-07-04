using ElectricalEngineerTools.DAL.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectricalEngineerTools.DAL.Entities
{
    public class LightingFixture : BaseEntity
    {
        public Guid? CableId { get; set; }
        public string Manufacturer { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Power { get; set; }
        [Required]
        public string ClimaticModification { get; set; }
        [Required]
        public string DiffuserMaterial { get; set; }
        [Required]
        public string IP { get; set; }
        [Required]
        public bool IsFireproof { get; set; }       
        public Cable Cable { get; set; }
        [Required]
        public string LdtIesFile { get; set; }
        [Required]
        public string DwgFile { get; set; }
    }
}
