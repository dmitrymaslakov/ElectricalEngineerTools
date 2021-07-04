using System;
using System.Collections.Generic;
using System.Text;

namespace ElectricalEngineerTools.DAL.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
