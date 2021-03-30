using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectPhase1.Builders
{
    public class ConcreteTeacherBuilder : AbstractTeacherBuilder
    {
        Regex regex = new Regex(@"^[A-Za-z]+$");

        protected override void BuildID(Teacher teacher)
        {
            int idAsText = 0;
           bool idValue = false;
            while (!idValue)
            {
                Console.WriteLine("Enter an ID for the teacher to update");
                var idInput = Console.ReadLine();
                idValue = int.TryParse(idInput, out idAsText);
            }
                teacher.ID = idAsText;
        }

        public string ValidateName(String name)
        {
            var TestInvalidFormat = (regex.IsMatch(name));
            while (!TestInvalidFormat)
            {
                Console.WriteLine("Not a valid input name string.Please enter a valid name once again");
                name = Console.ReadLine();
                TestInvalidFormat = (regex.IsMatch(name));
            }
            return name;
        }

        protected override void BuildFirstName(Teacher teacher)
        {
            
            Console.WriteLine("Enter the FirstName for the teacher");
            var FName = Console.ReadLine();
            FName = ValidateName(FName);
            teacher.FirstName = FName;
        }

        protected override void BuildLastName(Teacher teacher)
        {
            Console.WriteLine("Enter the LastName for the teacher");
            var LName = Console.ReadLine();
            LName = ValidateName(LName);
            teacher.LastName = LName;
        }
 
    }
}
