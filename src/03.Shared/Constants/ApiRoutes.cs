namespace SupportTicketSystem.Shared.Constants
{
    public static class ApiRoutes
    {
        private const string BaseApi = "api/v{version:apiVersion}";

        public static class Auth
        {
            // Full routes for client usage
            public const string Login = $"{BaseApi}/auth/login";

            // Route segments for controller usage
            public const string Base = $"{BaseApi}/auth";
            public const string LoginSegment = "login";
        }

        public static class Users
        {
            public const string Base = $"{BaseApi}/users";
        }

        public static class Tickets
        {
            // Route segments for client usage
            public const string Base = $"{BaseApi}/tickets";
            public const string Report = $"{Base}/report";
            public const string Assign = $"{Base}/{{id}}/assign";

            // Route segments for controller usage
            public const string ReportSegment = "report";
            public const string AssignSegment = "{id}/assign";
        }

        public static class Dashboard
        {
            public const string Summary = $"{BaseApi}/dashboard/summary";
        }
    }
}