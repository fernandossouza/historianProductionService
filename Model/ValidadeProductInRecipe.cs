using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using historianproductionservice.Service.Interface;
namespace historianproductionservice.Model
{
    public class ValidadeProductInRecipe :ValidationAttribute
    {
         protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var service = (IProductsService) validationContext
                         .GetService(typeof(IProductsService));
            InputData model = validationContext.ObjectInstance as InputData;
            
 
            if (model == null)
                return new ValidationResult("Object null");

            var returnValidate = service.ValidateProductIdInRecipe(model.productId,model.productionOrderId,model.type);

            if(returnValidate == false)
                return new ValidationResult("product is not registered in recipe"); 
            
            return ValidationResult.Success;
        }

        private string GetErrorMessage(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(this.ErrorMessage))
                return this.ErrorMessage; 
 
            return $"{validationContext.DisplayName} ";
        }

    }
}