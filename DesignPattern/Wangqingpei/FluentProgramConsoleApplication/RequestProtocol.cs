namespace FluentProgramConsoleApplication
{
    public class RequestProtocol
    {
        public const string Soap = "Soap";
        public const string InternalSoa = "Soa";
        private string _protocol;
        public RequestProtocol(string protocol)
        {
            this._protocol = protocol;
        }
    }
}