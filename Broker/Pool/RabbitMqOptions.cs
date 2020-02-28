namespace Broker.Pool
{
    public class RabbitMqOptions
    {
        #region Public Properties

        public string HostName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string VHost { get; set; }

        #endregion Public Properties
    }
}