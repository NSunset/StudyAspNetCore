using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.MyAttribute
{
    public class ValidateResult
    {
        public bool IsOk { get; set; }

        public string ErrorMsg { get; set; }

        public static ValidateResult Error(string errorMsg)
        {
            return new ValidateResult
            {
                IsOk = false,
                ErrorMsg = errorMsg
            };
        }

        public static ValidateResult Ok()
        {
            return new ValidateResult
            {
                IsOk = true,
                ErrorMsg = string.Empty
            };
        }
    }
}
