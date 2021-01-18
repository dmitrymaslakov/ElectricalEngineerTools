using ElectricalEngineerTools.DAL.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectricalEngineerTools.DAL.Entities
{
    public class LightingFixture : IEntity, INotifyPropertyChanged
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public string Manufacturer { get; set; }
        [Required]
        public string Brand { get; set; }
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
        public Cable Cable { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
