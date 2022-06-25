using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.MyAttribute
{
    public class LengthValidateAttribute : BaseValidateAttribute
    {
        public int MaxLength { get; set; }

        public int MinLength { get; set; }

        public override ValidateResult Validate(string propertyName, object value)
        {
            if (value.ToString().Length >= MinLength && value.ToString().Length <= MaxLength)
            {
                return ValidateResult.Ok();
            }
            return ValidateResult.Error($"{MinLength}<={propertyName}长度应该<={MaxLength}");
        }
    }
}
