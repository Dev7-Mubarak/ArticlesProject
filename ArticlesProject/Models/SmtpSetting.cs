namespace ArticlesProject.Models
{
    public class SmtpSetting
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
    }
}
