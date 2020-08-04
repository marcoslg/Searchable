//using Authorize.Application.Contracts;
//using MediatR;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Authorize.Application.Behaviours
//{
//    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
//    {
//        private readonly Stopwatch _timer;
//        private readonly ILogger<TRequest> _logger;
//        private readonly ICurrentUserService _currentUserService;
        

//        public RequestPerformanceBehaviour(
//            ILogger<TRequest> logger,
//            ICurrentUserService currentUserService)
//        {
//            _timer = new Stopwatch();
//            _logger = logger;
//            _currentUserService = currentUserService;
//        }

//        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
//        {
//            _timer.Start();

//            var response = await next();

//            _timer.Stop();

//            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

//            if (elapsedMilliseconds > 500)
//            {
//                var requestName = typeof(TRequest).Name;                
//                var userName = _currentUserService.UserName ?? string.Empty;

//                _logger.LogWarning("Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserName} {@UserName} {@Request}",
//                    requestName, elapsedMilliseconds, userName, userName, request);
//            }

//            return response;
//        }
//    }
//}
