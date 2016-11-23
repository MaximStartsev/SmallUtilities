using System.Collections.Generic;

namespace MaximStartsev.SmallUtilities.FoodSelector.Models
{
    class Tag
    {
        public Tag()
        {
            Dishes = new List<Dish>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Dish> Dishes { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
