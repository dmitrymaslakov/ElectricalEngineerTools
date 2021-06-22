using ElectricalEngineerTools.Framework.DAL;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Linq;
using System.Windows;
using ElectricalEngineerTools.Framework.DAL.Entities;
using System.Text;

namespace ElectricalEngineerTools.Tab.LightingAdmin.PL.Services
{
    public class DataConverter : IMultiValueConverter
    {
        const string MANUFACTURER_PARAMETER = "Manufacturer";
        const string LIGHT_SOURCE_INFO_PARAMETER = "LightSourceInfo";
        const string TECHNICAL_SPECIFICATIONS_PARAMETER = "TechnicalSpecifications";
        const string MOUNTING_PARAMETER = "Mounting";
        const string CLIMATE_APPLICATION_PARAMETER = "ClimateApplication";
        const string DIFFUSER_MATERIAL_PARAMETER = "DiffuserMaterial"; 
        const string IP_PARAMETER = "IP";
        const string EQUIPMENT_CLASS_PARAMETER = "EquipmentClass";
        const string DIMENSIONS_PARAMETER = "Dimensions";
        const string CABLES_PARAMETER = "Cable";

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var id = values[0] as int?;
                if (id is null) return "";
                //var context = ((ElectricsContext)values[1]);
                var context = new ElectricsContext();

                var result = "";
                switch (parameter as string)
                {
                    case MANUFACTURER_PARAMETER:
                        //result = context.Manufacturers.SingleOrDefault(m => m.Id.Equals((int)id))?.Name;
                        result = id == null ? "" : context.Database.SqlQuery<Manufacturer>($"SELECT * FROM {nameof(context.Manufacturers)} where Id={id}").SingleOrDefault()?.Name;
                        break;
                    case LIGHT_SOURCE_INFO_PARAMETER:
                        result = context.LightSourceInfoes.SingleOrDefault(l => l.Id.Equals((int)id))?.FullDescription;
                        //result = id == null ? "" : context.Database.SqlQuery<LightSourceInfo>($"SELECT * FROM {nameof(context.LightSourceInfoes)} where Id={id}").SingleOrDefault()?.FullDescription;
                        break;
                    case TECHNICAL_SPECIFICATIONS_PARAMETER:
                        result = context.TechnicalSpecifications.SingleOrDefault(t => t.Id.Equals((int)id))?.Number;
                        //result = id == null ? "" : context.Database.SqlQuery<TechnicalSpecifications>($"SELECT * FROM {nameof(context.TechnicalSpecifications)} where Id={id}").SingleOrDefault()?.Number;
                        break;
                    case MOUNTING_PARAMETER:
                        result = context.Mountings.SingleOrDefault(m => m.Id.Equals((int)id))?.FullDescription;
                        //result = id == null ? "" : context.Database.SqlQuery<Mounting>($"SELECT * FROM {nameof(context.Mountings)} where Id={id}").SingleOrDefault()?.FullDescription;
                        break;
                    case CLIMATE_APPLICATION_PARAMETER:
                        result = context.ClimateApplications.SingleOrDefault(c => c.Id.Equals((int)id))?.Value;
                        //result = id == null ? "" : context.Database.SqlQuery<ClimateApplication>($"SELECT * FROM {nameof(context.ClimateApplications)} where Id={id}").SingleOrDefault()?.Value;
                        break;
                    case DIFFUSER_MATERIAL_PARAMETER:
                        result = context.DiffuserMaterials.SingleOrDefault(d => d.Id.Equals((int)id))?.Description;
                        //result = id == null ? "" : context.Database.SqlQuery<DiffuserMaterial>($"SELECT * FROM {nameof(context.DiffuserMaterials)} where Id={id}").SingleOrDefault()?.Description;
                        break;
                    case IP_PARAMETER:
                        result = context.IngressProtections.SingleOrDefault(d => d.Id.Equals((int)id))?.Value.ToString();
                        //result = id == null ? "" : context.Database.SqlQuery<IngressProtection>($"SELECT * FROM {nameof(context.IngressProtections)} where Id={id}").SingleOrDefault()?.Value.ToString();
                        break;
                    case EQUIPMENT_CLASS_PARAMETER:
                        result = context.EquipmentClasses.SingleOrDefault(d => d.Id.Equals((int)id))?.Value;
                        //result = id == null ? "" : context.Database.SqlQuery<EquipmentClass>($"SELECT * FROM {nameof(context.EquipmentClasses)} where Id={id}").SingleOrDefault()?.Value;
                        break;
                    case DIMENSIONS_PARAMETER:
                        var dimensions = context.Dimensions.SingleOrDefault(d => d.Id.Equals((int)id));
                        //var dimensions = context.Database.SqlQuery<Dimensions>($"SELECT * FROM {nameof(context.Dimensions)} where Id={id}").SingleOrDefault();
                        result = $"{dimensions.RealDimensions} ({dimensions.DimensionsOnDwg})";
                        break;
                    case CABLES_PARAMETER:
                        result = context.Cables.SingleOrDefault(c => c.Id.Equals((int)id))?.FullName;
                        //result = id == null ? "" : context.Database.SqlQuery<Cable>($"SELECT * FROM {nameof(context.Cables)} where Id={id}").SingleOrDefault()?.FullName;
                        break;
                }
                return result;
            }
            catch (Exception ex)
            {
                var exception = new StringBuilder(ex.Message);
                exception.Append($" {ex.TargetSite.DeclaringType.Name}.{ex.TargetSite.Name}");
                MessageBox.Show(exception.ToString());
                return null;
            }
        }
        
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[] { DependencyProperty.UnsetValue };
        }
    }
}
