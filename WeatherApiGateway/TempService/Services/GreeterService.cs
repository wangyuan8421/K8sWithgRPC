using Grpc.Core;
using TempService;

namespace TempService.Services
{
    public class GreeterService : Temp.TempBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<TempReply> GetTemp(TempRequest request, ServerCallContext context)
        {
            return Task.FromResult(new TempReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}