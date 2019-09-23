using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace Marcusca10.Samples.BuildingAccessNet.Web.Models
{
    public class ManageUserViewModel
    {
        public string Id { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Tenant { get; set; }
    }

    public class TenantViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public string Realm { get; set; }
    }

    public class TenantEditViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CallbackPath { get; set; }
        public string Caption { get; set; }
        [Url]
        public string Realm { get; set; }
        [Url]
        public string MetadataAddress { get; set; }
    }
}