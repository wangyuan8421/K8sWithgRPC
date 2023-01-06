namespace TempService.Services
{
    using Grpc.Core;
    using TempService;
    public class HistoryTemp : Temp.TempBase
    {
        private readonly ILogger<HistoryTemp> _logger;
        public HistoryTemp(ILogger<HistoryTemp> logger)
        {
            _logger = logger;
        }

        public override Task<TempReply> GetTemp(TempRequest request, ServerCallContext context)
        {
            return Task.FromResult(new TempReply
            {
                TempHistoryVal = -request.Tempval
            });
        }
    }
}