using System.ComponentModel.DataAnnotations;

namespace Integrador.Core.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class CardExpirationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            var _month = value.ToString().Split('/')[0];
            var _year = $"20{value.ToString().Split('/')[1]}";

            if (int.TryParse(_month, out var month) &&
                int.TryParse(_year, out var year))
            {
                var d = new DateTime(year, month, 1);
                return d > DateTime.UtcNow;
            }

            return false;
        }
    }
}
