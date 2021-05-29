using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectricalEngineerTools.Framework.DAL.Entities
{
    public class LightingFixture : BaseEntity
    {
        [Required]
        public virtual Manufacturer Manufacturer { get; set; }
        public int ManufacturerId { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public virtual LightSourceInfo LightSourceInfo { get; set; }
        public int LightSourceInfoId { get; set; }
        public virtual TechnicalSpecifications TechnicalSpecifications { get; set; }
        public int? TechnicalSpecificationsId { get; set; }
        [Required]
        public virtual Mounting Mounting { get; set; }
        public int MountingId { get; set; }
        [Required]
        public virtual ClimateApplication ClimateApplication { get; set; }
        public int ClimateApplicationId { get; set; }
        [Required]
        public virtual DiffuserMaterial DiffuserMaterial { get; set; }
        public int DiffuserMaterialId { get; set; }
        [Required]
        public virtual IngressProtection IP { get; set; }
        public int IPId { get; set; }
        [Required]
        public EquipmentClass EquipmentClass { get; set; }
        public int EquipmentClassId { get; set; }
        [Required]
        public bool IsFireproof { get; set; }
        [Required]
        public bool IsExplosionProof { get; set; }
        [Required]
        public bool BPSU { get; set; }
        [Required]
        public virtual Dimensions Dimensions { get; set; }
        public int DimensionsId { get; set; }
        [Required]
        public string LdtIesFile { get; set; }
        public virtual Cable Cable { get; set; }
        public int? CableId { get; set; }
    }
}
