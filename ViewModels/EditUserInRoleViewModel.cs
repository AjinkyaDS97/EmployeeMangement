using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangment.ViewModels
{
    public class EditUserInRoleViewModel
    {
        public string UserId { get; set; }

        [JsonIgnore]
        public string UserName { get; set; }

        public bool IsSelected { get; set; }

    }
}
