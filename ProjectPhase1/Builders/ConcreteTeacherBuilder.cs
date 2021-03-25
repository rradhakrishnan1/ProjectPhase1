using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPhase1.Builders
{
    public class ConcreteTeacherBuilder : AbstractTeacherBuilder
    {
        protected override void BuildID(Teacher teacher)
        {
            Console.WriteLine("Enter an ID for the teacher");
            var idAsText = Console.ReadLine();
            teacher.ID = int.Parse(idAsText);
        }

        protected override void BuildFirstName(Teacher teacher)
        {
            Console.WriteLine("Enter the FirstName for the teacher");
            teacher.FirstName = Console.ReadLine();
  
        }

        protected override void BuildLastName(Teacher teacher)
        {
            Console.WriteLine("Enter the LastName for the teacher");
            teacher.LastName = Console.ReadLine();
        }
 
    }
}
