using Domain.DTOs;
using Domain.Entity.Sales;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOValidators
{
    public class SaleDtoValidator : AbstractValidator<SaleDto>
    {
        public SaleDtoValidator()
        {
            RuleFor(x => x.CustomerName).NotEmpty().WithMessage("Customer Name is required");
            RuleFor(x => x.CustomerPhoneNumber).NotEmpty().WithMessage("Phone Number is required");
            RuleFor(x => x.CustomerAddress).NotEmpty().WithMessage("Address is required");

            RuleFor(x => x.SaleDateTransferDto)
           .NotNull().WithMessage("Sale data is required")
           .SetValidator(new SaleDateTransferDtoValidator()!);

            RuleFor(x => x.SaleItemsDto)
                .NotNull().WithMessage("Sale items are required")
                .SetValidator(new SaleItemsDtoValidator()!);
        }
    }

    public class SaleDateTransferDtoValidator : AbstractValidator<SaleDateTransferDto>
    {
        public SaleDateTransferDtoValidator()
        {
            RuleFor(x => x.SaleDate).NotEmpty().WithMessage("Sale Date is required");

            RuleFor(x => x.PaymentStatus)
           .NotNull().WithMessage("Payment Status is required");

            RuleFor(x => x.PaymentMethod)
                .NotNull().WithMessage("Payment Method is required");
        }
    }

    public class SaleItemsDtoValidator : AbstractValidator<SaleItemsDto>
    {
        public SaleItemsDtoValidator()
        {
            RuleFor(x => x.ProductId)
            .NotNull().WithMessage("Product is required")
            .GreaterThan(0).WithMessage("Invalid ProductId");

            RuleFor(x => x.Quantity)
                .NotNull().WithMessage("Quantity is required")
                .GreaterThan(0).WithMessage("Quantity must be greater than zero");

            RuleFor(x => x.UnitPrice)
                .NotNull().WithMessage("Unit price is required")
                .GreaterThan(0).WithMessage("Unit price must be greater than zero");
        }
    }

}
