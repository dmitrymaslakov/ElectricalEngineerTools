using ElectricalEngineerTools.Framework.DAL.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElectricalEngineerTools.Framework.DAL.Entities
{
    public class Dimensions : BaseEntity
    {
        public Dimensions()
        {
            LightingFixtures = new HashSet<LightingFixture>();
        }

        public double? Length { get; set; }
        public double? Width { get; set; }
        public double? Diameter { get; set; }
        public double? LengthOnDwg { get; set; }
        public double? WidthOnDwg { get; set; }
        public double? DiameterOnDwg { get; set; }
        [NotMapped]
        public string RealDimensions
        {
            get
            {
                var dimensions = new StringBuilder();

                if (Length != null && Width != null)
                {
                    dimensions.Append($"{Length}х{Width}");
                }
                else if (Diameter != null)
                {
                    dimensions.Append($"Ø{Diameter}");
                }
                return dimensions.ToString();
            }
        }
        [NotMapped]
        public string DimensionsOnDwg
        {
            get
            {
                var dimensions = new StringBuilder();

                if (LengthOnDwg != null && WidthOnDwg != null)
                {
                    dimensions.Append($"{LengthOnDwg}х{WidthOnDwg}");
                }
                else if (DiameterOnDwg != null)
                {
                    dimensions.Append($"Ø{DiameterOnDwg}");
                }
                return dimensions.ToString();
            }
        }

        public virtual IEnumerable<LightingFixture> LightingFixtures { get; set; }
    }
}
