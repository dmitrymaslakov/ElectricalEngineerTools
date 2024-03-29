﻿using ElectricalEngineerTools.Framework.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElectricalEngineerTools.Framework.DAL.Entities
{
    public abstract class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}
