namespace PalTracker
{
    public class CloudFoundryInfo
    {
        public string Port { get; set; }  
        public string MemoryLimit { get; set; }
        public string CfInstanceIndex { get; set; }
        public string CfInstanceAddr { get; set; }

        public CloudFoundryInfo(string port, string memoryLimit, string cfInstanceIndex, string cfInstanceAddress)
        {
            this.Port = port;
            this.MemoryLimit = memoryLimit;
            this.CfInstanceIndex = cfInstanceIndex;
            this.CfInstanceAddr = cfInstanceAddress;
        }
    }
}