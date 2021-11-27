namespace SmartSolution.Infrastructure
{
    public class SmartSolutionSettings
    {
        public string ConnectionString { get; set; }

        public string JwtKey { get; set; }

        public string JwtIssuer { get; set; }

        public string JwtExpireMinutes { get; set; }

        public string ReportingServiceUrl { get; set; }

        public string ReportingServiceApiKey { get; set; }

        public string ServiceBusConnectionString { get; set; }

        public string EmailQueueName { get; set; }

        public string MikroIntegrationTopicName { get; set; }

        public string StorageAccountName { get; set; }

        public string StorageAccountKey { get; set; }

        public string BlobContainerName { get; set; }

        public string AssetsContainerName { get; set; }

        public string StampKeyPrefix { get; set; }

        public string SignKeyPrefix { get; set; }

        public int? FinancingInvoiceRecapId { get; set; }

        public decimal? CorrectionAmountMaxValue { get; set; }

        public decimal? CorrectionAmountMaxPercent { get; set; }

        public string Endpoint { get; set; }

        public string AccessKey { get; set; }

        public string Secret { get; set; }
    }
}
