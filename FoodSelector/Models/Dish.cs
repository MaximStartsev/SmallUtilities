using System.Collections.Generic;

namespace MaximStartsev.SmallUtilities.FoodSelector.Models
{
    class Dish
    {
        public Dish()
        {
            Ingredient = new List<Ingredient>();
            Tags = new List<Tag>();
        }
        public int Id { get; set; }
        public DishType Type { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// Количество использований. Чем меньше число, тем выше вероятность, что будет выбрано это блюдо
        /// </summary>
        public int Count { get; set; }
        public List<Ingredient> Ingredient { get; set; }
        public List<Tag> Tags { get; set; }
        public override string ToString()
        {
            return Title;
        }
    }
}
