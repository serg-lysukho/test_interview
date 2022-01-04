namespace InterviewApp.Configuration
{
    public interface IInterviewAppConfiguration
    {
        public string ImdbApiKey { get; }
        public string ImdbClientName { get; }

        public string CorezoidApiLogin { get; }
        public string CorezoidApiKey { get; }
        public string CorezoidClientName { get; }
        
        public string SmtpSenderName { get; }
        public string SmtpSenderMail { get; }
        public string SmtpHost { get; }
        public int SmtpPort { get; }
        public string MailPassword { get; }
    }
}