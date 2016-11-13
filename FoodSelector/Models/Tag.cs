namespace MaximStartsev.SmallUtilities.FoodSelector.Models
{
    class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
