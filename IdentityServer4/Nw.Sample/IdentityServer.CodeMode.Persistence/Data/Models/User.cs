using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.CodeMode.Persistence.Data.Models
{
    public class User
    {
        //
        // 摘要:
        //     Gets or sets the subject identifier.
        public int Id { get; set; }
        //
        // 摘要:
        //     Gets or sets the username.
        public string Username { get; set; }
        //
        // 摘要:
        //     Gets or sets the password.
        public string Password { get; set; }
        //
        // 摘要:
        //     Gets or sets the provider name.
        public string ProviderName { get; set; }
        //
        // 摘要:
        //     Gets or sets the provider subject identifier.
        public string ProviderSubjectId { get; set; }
        //
        // 摘要:
        //     Gets or sets if the user is active.
        public bool IsActive { get; set; }
        //
        // 摘要:
        //     Gets or sets the claims.
        //public ICollection<Claim> Claims { get; set; }
    }
}
