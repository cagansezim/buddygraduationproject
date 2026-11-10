using buddy.Debugging;

namespace buddy
{
    public class buddyConsts
    {
        public const string LocalizationSourceName = "buddy";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "6c5410f08e0940a39eccda06a38d534b";
    }
}
