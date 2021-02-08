using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangment.ViewModels
{
    public class UserClaimsViewModel
    {
        public UserClaimsViewModel()
        {
            Claims = new List<UserClaims>();
        }

        public string UserId { get; set; }

        public List<UserClaims> Claims { get; set; }
    }
}
