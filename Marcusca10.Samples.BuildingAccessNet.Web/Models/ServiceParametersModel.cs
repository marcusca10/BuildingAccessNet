using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Marcusca10.Samples.BuildingAccessNet.Web.Models
{
    [Table("ServiceParameters")]
    public class ServiceParametersModel
    {
        public Guid Id { get; set; }
        public bool IsFirstRun { get; set; }
        public bool EnableRegistration { get; set; }
    }
}