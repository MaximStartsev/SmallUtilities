using System.Collections.Generic;

namespace MaximStartsev.SmallUtilities.FoodSelector.Models
{
    class Ingredient
    {
        public int Id { get; set; }
        public int Title { get; set; }
        public List<Tag> Tag { get; set; }
    }
}
