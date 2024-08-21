namespace Library.Dtos
{
    public class UserDto
    {
        public long Id {get;set;}
        public string Username {get;set;}=string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

    }
}
