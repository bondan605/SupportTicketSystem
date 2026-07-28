namespace SupportTicketSystem.Shared.Constants
{
    public static class ApiRoutes
    {
        private const string BaseApi = "api";

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
            public const string Role = $"{Base}/{{role}}";
            public const string Agent = $"{Base}/agents" ;

            public const string AgentSegment = "agents";
        }

        public static class Tickets
        {
            // Route segments for client usage
            public const string Base = $"{BaseApi}/tickets";
            public const string Report = $"{Base}/report";
            public const string List = $"{Base}/list";
            public const string Assign = $"{Base}/{{id}}/assign";

            // Route segments for controller usage
            public const string ReportSegment = "report";
            public const string ListSegment = "list";
            public const string AssignSegment = "{id}/assign";
        }

        public static class Dashboard
        {
            public const string Summary = $"{BaseApi}/dashboard/summary";
        }

        public static class Report
        {
            public const string Reports = $"{BaseApi}/reports";
        }
    }
}