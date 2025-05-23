namespace MakeYourRestaurant___Main.Model
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

}
