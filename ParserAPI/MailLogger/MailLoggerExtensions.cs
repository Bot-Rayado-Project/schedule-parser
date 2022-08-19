public static class MailLoggerExtensions
{
    public static ILoggingBuilder AddMail(this ILoggingBuilder builder, string? adress, string? password)
    {
        if (adress != null && password != null)
            builder.AddProvider(new MailLoggerProvider(adress, password));
        return builder;
    }
}