using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalEngineerTools.Framework.DAL
{
    public class ElectricsContextFactory : IDbContextFactory<ElectricsContext>
    {
        public ElectricsContext Create()
        {
            return new ElectricsContext();
        }
    }
}
