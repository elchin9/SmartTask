namespace SmartSolution.Identity.ViewModels
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }
    }
}
