namespace WebApp.Strategy.Models
{
    public class Settings
    {
        public static string ClaimDataBaseType = "DatabaseType";

        public EDatabaseType DatabaseType;
        public EDatabaseType GetDatabaseType => EDatabaseType.SqlServer;
    }
}
