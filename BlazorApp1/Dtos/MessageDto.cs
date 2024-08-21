namespace Library.Dtos
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string FromUserName { get; set; }
        public int FromUserId { get; set; }
        public string ToUserName { get; set; }
        public int ToUserId { get; set; }
    }
}
