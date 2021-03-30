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
        int id = 0;
        ConcreteTeacherBuilder TeacherBuild = new ConcreteTeacherBuilder();
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

            bool idValue = false;
            while (!idValue)
            {
                Console.WriteLine("Enter an ID for the teacher to update");
                var idInput = Console.ReadLine();
                idValue = int.TryParse(idInput, out id);
            }

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

        protected override void searchTeacher()
        {
            bool idValue = false;
            while (!idValue)
            {
                Console.WriteLine("Enter an ID for the teacher to update");
                var idInput = Console.ReadLine();
                idValue = int.TryParse(idInput, out id);
            }

            if (!_teachers.ContainsKey(id))
            {
                Console.WriteLine($"Teacher with id {id} not found");
            }
            else
            {          
                Console.WriteLine($"Teacher with id { _teachers[id]}    is found");
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
            Console.WriteLine("1) ID by Ascending");
            Console.WriteLine("2) ID by Descending");
            Console.WriteLine("3) First Name by Ascending");
            Console.WriteLine("4) First Name by Descending");
            Console.WriteLine("5) Last Name by Ascending");
            Console.WriteLine("6) Last Name by Descending");

            var option = int.Parse(Console.ReadLine());
            ISortTeachersStrategy sortStrategy = null;
            switch (option)
            {
                case 1:
                    {
                        sortStrategy = new SortTeachersByIdStrategy();
                        var sorted = sortStrategy.SortByAsc(_teachers.Values);
                        listTeachers(sorted);
                        break;
                    }
                case 2:
                    {
                        sortStrategy = new SortTeachersByIdStrategy();
                        var sorted = sortStrategy.SortByDesc(_teachers.Values);
                        listTeachers(sorted);
                        break;
                    }
                case 3:
                    {
                        sortStrategy = new SortTeachersByFirstNameStrategy();
                        var sorted = sortStrategy.SortByAsc(_teachers.Values);
                        listTeachers(sorted);
                        break;
                    }
                  
                case 4:
                    {
                        sortStrategy = new SortTeachersByFirstNameStrategy();
                        var sorted = sortStrategy.SortByDesc(_teachers.Values);
                        listTeachers(sorted);
                        break;
                    }
                case 5:
                    {
                        sortStrategy = new SortTeachersByLastNameStrategy();
                        var sorted = sortStrategy.SortByAsc(_teachers.Values);
                        listTeachers(sorted);
                        break;
                    }
                case 6:
                    {
                        sortStrategy = new SortTeachersByLastNameStrategy();
                        var sorted = sortStrategy.SortByDesc(_teachers.Values);
                        listTeachers(sorted);
                        break;
                    }


            }
            
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
            bool idValue = false;
            while (!idValue)
            {
                Console.WriteLine("Enter an ID for the teacher to update");
                var idInput = Console.ReadLine();
                idValue = int.TryParse(idInput, out id);
            }
        
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
                NewFirstName = TeacherBuild.ValidateName(NewFirstName);
                _teachers[id].FirstName = NewFirstName;

                Console.WriteLine("Enter new LastName of the selected teacher to update");
                var NewLastName = Console.ReadLine();
                NewLastName = TeacherBuild.ValidateName(NewLastName);
                _teachers[id].LastName = NewLastName;

                Console.WriteLine("UpdatedFirstName and  LastName of the selected teacher");
                Console.WriteLine(teacher);
            }

            else if (option == 1)
            {
                Console.WriteLine("Enter new FirstName of the selected teacher to update");
                var NewFirstName = Console.ReadLine();
                NewFirstName = TeacherBuild.ValidateName(NewFirstName);
                _teachers[id].FirstName = NewFirstName;

                Console.WriteLine("Updated FirstName of the selected teacher");
                Console.WriteLine(teacher);

            }
            else if (option == 2)
            {
                Console.WriteLine("Enter new LastName of the selected teacher to update");
                var NewLastName = Console.ReadLine();
                NewLastName = TeacherBuild.ValidateName(NewLastName);
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
