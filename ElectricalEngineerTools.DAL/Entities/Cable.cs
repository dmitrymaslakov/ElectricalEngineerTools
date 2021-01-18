using ElectricalEngineerTools.DAL.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectricalEngineerTools.DAL.Entities
{
    public class Cable : IEntity, INotifyPropertyChanged
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public int CoresNumber { get; set; }
        [Required]
        public double Section { get; set; }

        public Guid LightingFixtureId { get; set; }
        public LightingFixture LightingFixture { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
