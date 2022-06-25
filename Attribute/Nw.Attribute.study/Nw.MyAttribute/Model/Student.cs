using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.MyAttribute
{
    public class Student
    {
        [Remark("主键Id")]
        public int Id { get; set; }

        [LengthValidate(MaxLength = 5, MinLength = 3)]
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Remark("性别")]
        public Sex Sex { get; set; }


    }

    public enum Sex
    {
        /// <summary>
        /// 男
        /// </summary>
        [Remark("男")]
        Male,

        /// <summary>
        /// 女
        /// </summary>
        [Remark("女")]
        Female
    }
}
