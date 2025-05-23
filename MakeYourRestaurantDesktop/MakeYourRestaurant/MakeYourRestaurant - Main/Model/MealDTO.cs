namespace MakeYourRestaurant___Main.Model
{
    public class MealDTO
    {
        public int ID { get; set; }
        public int RestaurantId { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public string Details { get; set; }
        public bool BestFood { get; set; }
        public string MenuType { get; set; }
        public string PhotoFile { get; set; }
    }
}
