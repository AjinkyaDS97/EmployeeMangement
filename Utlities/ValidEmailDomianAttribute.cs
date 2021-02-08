using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangment.Utlities
{
    public class ValidEmailDomianAttribute : ValidationAttribute
    {
        private readonly string allowDomain;

        public ValidEmailDomianAttribute(string allowDomain)
        {
            this.allowDomain = allowDomain;
        }
        public override bool IsValid(object value)
        {
           string[] stringArr= value.ToString().Split('@');
            return stringArr[1].ToUpper() == allowDomain.ToUpper();
        }
    }
}
