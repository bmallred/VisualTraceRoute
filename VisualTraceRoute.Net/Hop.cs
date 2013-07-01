using System.Net;

namespace VisualTraceRoute.Net
{
    /// <summary>
    /// Trace route hop.
    /// </summary>
    public class Hop
    {
        private IPAddress _address;
        private int _hop;
        private long _roundTrip;

        /// <summary>
        /// Gets the Internet address in standard notation.
        /// </summary>
        public string Address
        {
            get { return this._address.ToString(); }
        }

        /// <summary>
        /// Gets the hop count.
        /// </summary>
        public int HopCount
        {
            get { return this._hop; }
        }

        /// <summary>
        /// Gets the Internet address host name.
        /// </summary>
        public string HostName
        {
            get 
            {
                string hostName;

                try
                {
                    hostName = Dns.GetHostEntry(this._address).HostName;
                }
                catch
                {
                    hostName = this._address.ToString();
                }

                return hostName;
            }
        }

        /// <summary>
        /// Gets the total round trip time (in milliseconds).
        /// </summary>
        public long RoundTrip
        {
            get { return this._roundTrip; }
        }

        /// <summary>
        /// Initializes a new instance of the Hop class.
        /// </summary>
        /// <param name="Address">Internet address.</param>
        /// <param name="RoundTrip">Round trip time in milliseconds.</param>
        /// <param name="Hop">Hop count.</param>
        public Hop(IPAddress Address, long RoundTrip, int Hop)
        {
            this._address = Address;
            this._hop = Hop;
            this._roundTrip = RoundTrip;
        }

        /// <summary>
        /// Write the trace route reply in standard output format.
        /// </summary>
        /// <returns>Trace hop.</returns>
        public override string ToString()
        {
            return this.ToString(false);
        }

        /// <summary>
        /// Write the trace route reply in standard output format.
        /// </summary>
        /// <param name="ResolveHostNames">A value indicating whether to resolve the hostname of the reply.</param>
        /// <returns>Trace route hop.</returns>
        public string ToString(bool ResolveHostNames)
        {
            return string.Format
                (
                    "{0}\t{1}ms\t{2}{3}", 
                    this._hop.ToString(), 
                    this._roundTrip.ToString(), 
                    ResolveHostNames ? string.Format("[{0}] ", this.HostName) : string.Empty, 
                    this._address.ToString()
                );
        }
    }
}
