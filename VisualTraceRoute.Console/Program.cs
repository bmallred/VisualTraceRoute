using System.Linq;
using VisualTraceRoute.Net;
using VisualTraceRoute.Text;

namespace VisualTraceRoute.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Initialize the parser.
            Parser parser = new Parser(@"..\..\..\..\VisualTraceRoute\VisualTraceRoute.Text\sample-console.txt");
            
            // Read the template in.
            parser.ReadTemplate();

            // Outputs the template to the console screen.
            parser.ToStream(System.Console.OpenStandardOutput(), new TraceRoute[] { TraceRoute.ByHostName("google.com") });

            System.Console.WriteLine("Press any key to continue...");
            System.Console.ReadKey();
        }
    }
}
