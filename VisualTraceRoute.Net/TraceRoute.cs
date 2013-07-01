using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace VisualTraceRoute.Net
{
    public class TraceRoute
    {
        private IPAddress _destination;
        private Collection<Hop> _hops;
        private int _maxHops;

        /// <summary>
        /// Gets the trace route destination.
        /// </summary>
        public string Destination
        {
            get { return this._destination.ToString(); }
        }

        /// <summary>
        /// Gets the trace route hops.
        /// </summary>
        public Collection<Hop> Hops
        {
            get { return this._hops; }
        }

        /// <summary>
        /// Initializes a new instance of the TraceRoute class.
        /// </summary>
        /// <param name="destination">Destination address.</param>
        /// <param name="maxHops">Maximum hops.</param>
        /// <param name="execute">A value indicating whether to execute the trace immediately.</param>
        public TraceRoute(string destination, int maxHops = 30, bool execute = false)
            : this(Dns.GetHostEntry(destination).AddressList.FirstOrDefault(), maxHops, execute)
        {
            // Stub.
        }

        /// <summary>
        /// Initializes a new instance of the TraceRoute class.
        /// </summary>
        /// <param name="destination">Destination address.</param>
        /// <param name="maxHops">Maximum hops.</param>
        /// <param name="execute">A value indicating whether to execute the trace immediately.</param>
        public TraceRoute(IPAddress destination, int maxHops = 30, bool execute = false)
        {
            this._destination = Dns.GetHostEntry(destination).AddressList.FirstOrDefault();
            this._hops = new Collection<Hop>();
            this._maxHops = maxHops;

            if (execute)
            {
                this._hops.AddRange(GetTraceRoute(this._destination, this._maxHops));
            }
        }

        /// <summary>
        /// Initiate the trace route processes.
        /// </summary>
        /// <param name="hostname">Destination address.</param>
        /// <param name="maxHops">Maximum number of hops to perform.</param>
        /// <returns>Trace route.</returns>
        public static TraceRoute ByHostName(string hostname, int maxHops = 30)
        {
            return new TraceRoute(hostname, maxHops, true);
        }

        /// <summary>
        /// Initiate the trace route processes.
        /// </summary>
        /// <param name="address">Destination address.</param>
        /// <param name="maxHops">Maximum number of hops to perform.</param>
        /// <returns>Trace route.</returns>
        public static TraceRoute ByAddress(IPAddress address, int maxHops = 30)
        {
            return new TraceRoute(address.ToString(), maxHops, true);
        }

        /// <summary>
        /// Perform the trace route processes.
        /// </summary>
        /// <param name="address">Destination address.</param>
        /// <param name="maxHops">Maximum number of hops to perform.</param>
        /// <returns>Enumerable array of trace route replies.</returns>
        private static IEnumerable<Hop> GetTraceRoute(IPAddress address, int maxHops)
        {
            byte[] buffer = Encoding.ASCII.GetBytes("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            bool finalDestination = false;
            int timeout = 10000;
            int ttl = 1;

            while (ttl < maxHops + 1 && !finalDestination)
            {
                using (Ping p = new Ping())
                {
                    PingOptions options = new PingOptions(ttl, true);
                    PingReply reply = p.Send(address, timeout, buffer, options);

                    if (reply.Address != null)
                    {
                        // Determine if this is the final destination (which will stop the pings).
                        finalDestination = reply.Address.ToString().Equals(address.ToString());

                        // If the time-to-live expired ping again to get a good round trip.
                        if (reply.Status == IPStatus.TtlExpired)
                        {
                            reply = p.Send(reply.Address);
                        }

                        // Return the latest and greatest.
                        yield return new Hop(reply.Address, reply.RoundtripTime, ttl);

                        // Output to a trace stream.
                        Trace.WriteLine(string.Empty);
                        Trace.WriteLine(string.Format("Status: {0}", reply.Status));
                        Trace.WriteLine(string.Format("{0}{1}{2}ms", ttl.ToString().PadRight(3), reply.Address.ToString(), reply.RoundtripTime.ToString().PadLeft(5)));
                    }

                    ttl++;
                }
            }
        }
    }
}
