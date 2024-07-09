using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchopenSozlukEntityLayer.Concrete
{
    public class AppRole : IdentityRole<int>
    {
        public static implicit operator IdentityRole(AppRole appRole)
        {
            var identityRole = new IdentityRole
            {
                Id = appRole.Id.ToString(),
                Name = appRole.Name,
                NormalizedName = appRole.NormalizedName
                // Diğer gerekli özellikleri buraya ekleyebilirsiniz
            };
            return identityRole;
        }
    }
}
