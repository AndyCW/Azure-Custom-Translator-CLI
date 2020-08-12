using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
public class ListValidatorAttribute : ValidationAttribute
{
    private readonly List<string> _listValues;

    public ListValidatorAttribute(string values) => _listValues = new List<string>(values.Split('|'));

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (!_listValues.Contains(value.ToString().ToLower()))
        {
            return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
        }
        return null;
    }
}