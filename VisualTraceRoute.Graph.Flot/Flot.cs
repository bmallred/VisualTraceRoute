using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using VisualTraceRoute.Net;
using VisualTraceRoute.Text;

namespace VisualTraceRoute.Graph.Flot
{
    /// <summary>
    /// Flot form.
    /// </summary>
    public partial class Flot : Form
    {
        /// <summary>
        /// Initializes a new instance of the Flot class.
        /// </summary>
        public Flot()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Form load.
        /// </summary>
        /// <param name="sender">Calling object.</param>
        /// <param name="e">Event arguments.</param>
        private void Flot_Load(object sender, EventArgs e)
        {
            // Load the trace HTML on loading.
            this.browser.Url = new System.Uri(new FileInfo(Path.Combine("Resources", "trace.html")).FullName);
        }

        /// <summary>
        /// Handle the Enter key press.
        /// </summary>
        /// <param name="sender">Calling object.</param>
        /// <param name="e">Key event arguments.</param>
        private void address_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.StartTrace();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Begin trace route.
        /// </summary>
        /// <param name="sender">Calling object.</param>
        /// <param name="e">Event arguments.</param>
        private void traceRoute_Click(object sender, EventArgs e)
        {
            this.StartTrace();
        }

        /// <summary>
        /// Start a trace with the form data.
        /// </summary>
        private void StartTrace()
        {
            string addr = this.address.Text;

            // Ensure there is an address present.
            if (!string.IsNullOrEmpty(addr))
            {
                List<TraceRoute> routes = new List<TraceRoute>();
                routes.Add(TraceRoute.ByHostName(addr));

                foreach (TraceRoute route in routes)
                {
                    foreach (Hop hop in route.Hops)
                    {
                        this.console.AppendText(string.Format("{0}{1}", hop, Environment.NewLine));
                    }
                }

                // Map to the files.
                FileInfo templateFile = new FileInfo(Path.Combine("Resources", "flot-template.txt"));
                FileInfo jsonFile = new FileInfo(Path.Combine("Resources", "trace.html"));

                // Operate the text parser.
                Parser parser = new Parser(templateFile);
                parser.ReadTemplate();
                parser.Write(jsonFile, routes.ToArray());

                // Refresh contents.
                this.browser.Refresh();
            }

            this.address.Focus();
        }
    }
}
