using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.Base.Entities.SaaS
{
    [Keyless]
    public class View_TenantUserPlan
    {
        public string Email { get; set; } = null!;
        public string AzureAdUserId { get; set; } = null!;
        public string Plan { get; set; } = null!;
        public string TenantPublicId { get; set; } = null!;
    }
}
