using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.Create
{
    public class Student : Singleton<Student>
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public Student()
        {
            Console.WriteLine($"{nameof(Student)}被构造");
        }

        /// <summary>
        /// 创建重复对象
        /// </summary>
        /// <returns></returns>
        public static Student GetStudentClone()
        {
            return t.MemberwiseClone() as Student;
        }
    }
}
