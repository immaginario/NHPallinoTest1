using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using NHPallinoTest1.Model;
using NHPallinoTest1.NHMappings;

namespace NHPallinoTest1.Test
{
    public class NHHelper
    {
        public static FluentMappingsContainer BuildMappings(MappingConfiguration mappingConfiguration)
        {
            var mapping = mappingConfiguration.FluentMappings
                            .AddFromAssemblyOf<ShopMapping>();
            return mapping;
        }
    }
}
