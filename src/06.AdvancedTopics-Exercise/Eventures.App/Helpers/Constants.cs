namespace Eventures.App.Helpers
{
    public class Constants
    {
        public const string UsernameLengthErrorMessage = "Username must contain a minimum of 3 symbols";

        public const string UsernameRegexErrorMessage = "Username may contain only alphanumeric characters, dashes, underscores, dots, asterisks and tildes";

        public const string UcnErrorMessage = "UCN must contain exactly 10 digits";

        public const string PasswordLengthErrorMessage = "Password must contain a minimum of 5 symbols";

        public const string EventNameLengthErrorMessage = "Name must contain a minimum of 10 symbols";

        public const string EventStartErrorMessage = "Event start date must be a valid date";

        public const string EventEndErrorMessage = "Event end date must be a valid date";

        public const string OrderErrorMessage = "Please enter a vlid positive integer";
    }
}
