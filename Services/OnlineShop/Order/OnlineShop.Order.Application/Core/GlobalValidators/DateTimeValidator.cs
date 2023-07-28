using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Order.Application.Core.GlobalValidators
{
    public class DateTimeWithTimeZoneValidator : AbstractValidator<DateTime>
    {
        public DateTimeWithTimeZoneValidator()
        {
            RuleFor(dateTime => dateTime.Kind)
                .Equal(DateTimeKind.Utc)
                .WithMessage("The date must be in DateTime with time zone format (UTC).");
        }
    }
}