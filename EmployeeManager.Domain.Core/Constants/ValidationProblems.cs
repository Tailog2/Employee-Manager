using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Domain.Core.Constants
{
    public class ValidationProblems
    {
        public const string FullNameСollision = "An employee with such first name and last name already exist";
        public const string CioСollision = "The company already has CIO";
        public const string FullNameOnlyLetters = "An employee first name and last name must contain only letters";
    }
}
