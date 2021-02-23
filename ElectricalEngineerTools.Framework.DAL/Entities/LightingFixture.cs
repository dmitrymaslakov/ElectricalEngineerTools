using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectricalEngineerTools.Framework.DAL.Entities
{
    public class LightingFixture : BaseEntity
    {
        public string Manufacturer { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Description { get; set; }
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
        [Required]
        public bool BPSU { get; set; }
        [Required]
        public string LightSourceType { get; set; }
        [Required]
        public string Length { get; set; }
        [Required]
        public string Width { get; set; }
        [Required]
        public string LdtIesFile { get; set; }
        [Required]
        public string DwgFile { get; set; }
        
        public Guid? CableId { get; set; }
        public virtual Cable Cable { get; set; }
    }
}
