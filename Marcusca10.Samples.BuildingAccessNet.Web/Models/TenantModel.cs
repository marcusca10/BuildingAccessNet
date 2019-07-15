using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Marcusca10.Samples.BuildingAccessNet.Web.Models
{
    public class TenantModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public string MetadataAddress{ get; set; }
        public string Realm { get; set; }
    }
}