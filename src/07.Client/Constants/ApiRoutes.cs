namespace SupportTicketSystem.Client.Constants
{
    public static class ApiRoutes
    {
        private const string BaseApi = "api/v1";

        public const string Categories = $"{BaseApi}/category";
        public const string Accounts = $"{BaseApi}/account";
        public const string Transactions = $"{BaseApi}/transaction";
        public const string Dashboard = $"{BaseApi}/dashboard";
        
    }
}
