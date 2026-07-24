//using SupportTicketSystem.Client.Features.Interfaces;

//namespace SupportTicketSystem.Client
//{
//    public class AuthHeaderHandler : DelegatingHandler
//    {
//        private readonly ITokenProvider _tokenProvider;

//        public AuthHeaderHandler(ITokenProvider tokenProvider)
//        {
//            _tokenProvider = tokenProvider;
//        }

//        protected override async Task<HttpResponseMessage> SendAsync(
//            HttpRequestMessage request, CancellationToken cancellationToken)
//        {
//            var token = await _tokenProvider.GetTokenAsync();
//            Console.WriteLine($"[AuthHeaderHandler] URL: {request.RequestUri}");
//            Console.WriteLine($"[AuthHeaderHandler] Token: {(string.IsNullOrEmpty(token) ? "NULL/EMPTY" : "Found, length=" + token.Length)}");

//            if (!string.IsNullOrWhiteSpace(token))
//            {
//                request.Headers.Authorization = new("Bearer", token);
//            }
//            return await base.SendAsync(request, cancellationToken);
//        }
//    }
//}