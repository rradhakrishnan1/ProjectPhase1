using ProjectPhase1.Builders;
using ProjectPhase1.Repositories;
using ProjectPhase1.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPhase1.Templates
{
    public class ConcreteTeacherManagementApp : AbstractTeacherAppTemplate
    {
        protected override void loadTeachers()
        {
            var teachersRepository = new PipeDelimitedFileTeachersRepository("teachers.txt");
            var teachersAsList = teachersRepository.Load();
            _teachers = teachersAsList.ToDictionary(t => t.ID);
            Console.WriteLine($"Loaded {_teachers.Count} teachers");
        }

        protected override void saveTeachers(IEnumerable<Teacher> teachers)
        {
            var teachersRepository = new PipeDelimitedFileTeachersRepository("teachers.txt");
            teachersRepository.Save(teachers);
        }

        protected override int getOption()
        {
            try
            {
                return (int.Parse(Console.ReadLine()));
            }
            catch(Exception e)
            {
                Console.WriteLine("Enter a  valid integer value from 1 to 9");
            }

            return -1;
        }

        protected override void addTeacher()
        {
            var teacherBuilder = new ConcreteTeacherBuilder();
            var teacher = teacherBuilder.Build();
            _teachers[teacher.ID] = teacher;
            Console.WriteLine("New Teacher added");

            saveTeachers(_teachers.Values);
        }

        protected override void deleteTeacher()
        {
            Console.WriteLine("Enter ID of techer to delete");
            var id = int.Parse(Console.ReadLine());

            if (!_teachers. ContainsKey(id))
            {
                Console.WriteLine($"Teacher with id {id} not found");
            }
            else
            {
                _teachers.Remove(id);
                Console.WriteLine("Removed teacher");
            }

            saveTeachers(_teachers.Values);

        }

        protected override void findTeacher()
        {
            Console.WriteLine("Enter ID of teacher to find");
            var id = int.Parse(Console.ReadLine());

            if (!_teachers.ContainsKey(id))
            {
                Console.WriteLine($"Teacher with id {id} not found");
            }
            else
            {          
                Console.WriteLine($"Teacher with id { _teachers[id]} is found");
            }
        }

        protected override void listTeachers(IEnumerable<Teacher> teachers)
        {
            foreach ( var teacher in teachers)
            {
                Console.WriteLine($"Teachers: {teacher.ID} {teacher.FirstName} {teacher.LastName}");
            }
        }

        protected override void sortTeachers()
        {
            Console.WriteLine("You chose to sort teachers");
            Console.WriteLine("How would you like to sort them?");
            Console.WriteLine("1) ID");
            Console.WriteLine("2) First Name");
            Console.WriteLine("3) Last Name");

            var option = int.Parse(Console.ReadLine());
            ISortTeachersStrategy sortStrategy = null;
            switch (option)
            {
                case 1: sortStrategy = new SortTeachersByIdStrategy(); break;
                case 2: sortStrategy = new SortTeachersByFirstNameStrategy(); break; 
                case 3: sortStrategy = new SortTeachersByLastNameStrategy(); break;
            }

            var sorted = sortStrategy.Sort(_teachers.Values);
            listTeachers(sorted);
            
        }

        private void displayUpdateMenu()
        {
            Console.WriteLine("\n");
            Console.WriteLine("Please select in the following options:");
            Console.WriteLine("1)FirstName");
            Console.WriteLine("2) LastName");
            Console.WriteLine("3) Both");
            Console.WriteLine("4) None");
        }

        protected override  int getUpdateOption()
        {
            Console.WriteLine("Do you want to update the Firstname or Lastname or both?");

            displayUpdateMenu();
            try
            {
                return (int.Parse(Console.ReadLine()));
            }
            catch (Exception e)
            {
                Console.WriteLine("Enter a  valid integer value from 1 to 4");
            }

            return -1;

        }      

        protected override void updateTeacher()
        {
            Console.WriteLine("Enter ID of teacher to update");
            var id = int.Parse(Console.ReadLine());
            if (!_teachers.ContainsKey(id))
            {
                Console.WriteLine($"Did not find teacher with id: {id}");
                return;
            }

            var teacher = _teachers[id];
            Console.WriteLine("You selected...");
            Console.WriteLine(teacher);


            var option = getUpdateOption();

            if (option == 3)
            {

                Console.WriteLine("Enter new FirstName of the selected teacher to update");
                var NewFirstName = Console.ReadLine();
                _teachers[id].FirstName = NewFirstName;

                Console.WriteLine("Updated FirstName of the selected teacher");
                Console.WriteLine(teacher);


                Console.WriteLine("Enter new LastName of the selected teacher to update");
                var NewLastName = Console.ReadLine();
                _teachers[id].LastName = NewLastName;

                Console.WriteLine("Updated LastName of the selected teacher");
                Console.WriteLine(teacher);
            }

            else if (option == 1)
            {
                Console.WriteLine("Enter new FirstName of the selected teacher to update");
                var NewFirstName = Console.ReadLine();
                _teachers[id].FirstName = NewFirstName;

                Console.WriteLine("Updated FirstName of the selected teacher");
                Console.WriteLine(teacher);

            }
            else if (option == 2)
            {
                Console.WriteLine("Enter new LastName of the selected teacher to update");
                var NewLastName = Console.ReadLine();
                _teachers[id].LastName = NewLastName;

                Console.WriteLine("Updated LastName of the selected teacher");
                Console.WriteLine(teacher);
            }

            else
            {
                Console.WriteLine("User not interested in updating the teacher details.Hence exiting.");
            }

                saveTeachers(_teachers.Values);
        }
    }
}
