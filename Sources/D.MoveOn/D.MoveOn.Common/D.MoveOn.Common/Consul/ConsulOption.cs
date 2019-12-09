namespace D.MoveOn.Common
{
    public class ConsulOption
    {
        public bool Enable { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
        public string PingEndPoint { get; set; }
        public int RemoveAfterInterval { get; set; }
        public int Interval { get; set; }
        public int PingInterval { get; set; }
        public bool PingEnabled { get; set; }
        public int RequestRetries { get; set; }
        public bool SkipLocalhostDockerDnsReplace { get; set; }
    }
}