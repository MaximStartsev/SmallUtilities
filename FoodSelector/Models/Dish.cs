using System.Collections.Generic;

namespace MaximStartsev.SmallUtilities.FoodSelector.Models
{
    class Dish
    {
        public DishType Type { get; set; }
        public string Title { get; set; }
        public List<Ingredient> Ingredient { get; set; }
        //todo отдельный класс
        public List<string> Tag { get; set; }
    }
}
