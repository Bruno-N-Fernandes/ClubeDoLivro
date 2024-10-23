using System;

namespace ClubeDoLivro.Domains
{
    public class AccessToken
    {
        public string Scheme { get; set; }
        public string Token { get; set; }
        public long ExpiresIn { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}