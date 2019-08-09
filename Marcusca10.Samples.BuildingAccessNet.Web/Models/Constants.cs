using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marcusca10.Samples.BuildingAccessNet.Web.Models
{
    public enum AppRoles
    {
        Owner,
        Admin
    }

    public enum ClaimNames
    {
        tenant
    }

    public enum ErrorMessages
    {
        NotProvisioned,
        NotInTenant,
        InvalidTenant,
        InvalidChange
    }
}