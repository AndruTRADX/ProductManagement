namespace ProductManagement.DTOs
{
    public class UserDTO
    {
        public Guid UserID { get; set; }
        public required string Name { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; } = "Customer";
    }
}
