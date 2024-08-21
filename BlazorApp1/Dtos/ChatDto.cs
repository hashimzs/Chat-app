﻿namespace Library.Dtos
{
    public class ChatDto
    {
        public int Id { get; set; }
        public string Email { get; set; }=string.Empty;
        public string Username { get; set; }=string.Empty;
        public long UnReadCount { get; set; }
    }
}
