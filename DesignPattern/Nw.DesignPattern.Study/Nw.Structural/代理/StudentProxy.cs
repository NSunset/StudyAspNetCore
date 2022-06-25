using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.Structural
{
    public class StudentProxy : ISubject
    {
        private Student _student = new Student();
        public void Add()
        {
            Console.WriteLine("在用户操作前记录日志");
            _student.Add();
        }

        public void Find(int id)
        {
            Console.WriteLine("在用户查询前检查权限");
            _student.Find(id);
        }
    }
}
