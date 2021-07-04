using ElectricalEngineerTools.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElectricalEngineerTools.DAL.Entities
{
    public abstract class BaseEntity : IEntity, INotifyPropertyChanged
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

    }
}
