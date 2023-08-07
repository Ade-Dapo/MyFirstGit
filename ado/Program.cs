
using ado;

StudentManager manager = new StudentManager();

// manager.CreateTable();
Student student = new Student{
    Id = 2,
    FirstName = "Fola",
    LastName = "Shade",
    Age = 23,
};

// manager.AddStudent(student);
// manager.GetAll();
// foreach (Student stu in manager.GetAll())
// {
//     Console.WriteLine($"{stu.Id}. {stu.LastName} {stu.FirstName} -{stu.Age}");
// }
 Student stu = manager.GetById(1);
Console.WriteLine(stu == null? "Record not found" : $"{stu.Id}. {stu.LastName} {stu.FirstName} -{stu.Age}");