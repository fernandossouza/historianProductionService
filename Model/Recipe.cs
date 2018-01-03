using System.Collections.Generic;

namespace historianproductionservice.Model
{
    public class Recipe
    {
        public int recipeId { get; set; }
        public string recipeName { get; set; }
        public string recipeCode { get; set; }
        public PhaseProduct recipeProduct { get; set; }
        public ICollection<Phase> phases { get; set; }
    }
}