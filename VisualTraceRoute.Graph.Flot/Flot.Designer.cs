namespace VisualTraceRoute.Graph.Flot
{
    /// <summary>
    /// Flot class.
    /// </summary>
    public partial class Flot
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">True if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.browser = new System.Windows.Forms.WebBrowser();
            this.address = new System.Windows.Forms.TextBox();
            this.traceRoute = new System.Windows.Forms.Button();
            this.console = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.71323F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.28676F));
            this.tableLayoutPanel1.Controls.Add(this.browser, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.address, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.traceRoute, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.console, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 73.52941F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.47059F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(544, 319);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // browser
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.browser, 2);
            this.browser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browser.Location = new System.Drawing.Point(3, 63);
            this.browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.browser.Name = "browser";
            this.browser.ScriptErrorsSuppressed = true;
            this.browser.Size = new System.Drawing.Size(538, 184);
            this.browser.TabIndex = 0;
            this.browser.Url = new System.Uri(string.Empty, System.UriKind.Relative);
            // 
            // address
            // 
            this.address.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.address.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.address.Location = new System.Drawing.Point(3, 15);
            this.address.Name = "address";
            this.address.Size = new System.Drawing.Size(394, 30);
            this.address.TabIndex = 0;
            this.address.Text = "localhost";
            this.address.KeyDown += new System.Windows.Forms.KeyEventHandler(this.address_KeyDown);
            // 
            // traceRoute
            // 
            this.traceRoute.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.traceRoute.Location = new System.Drawing.Point(422, 12);
            this.traceRoute.Name = "traceRoute";
            this.traceRoute.Size = new System.Drawing.Size(100, 35);
            this.traceRoute.TabIndex = 2;
            this.traceRoute.Text = "Trace";
            this.traceRoute.UseVisualStyleBackColor = true;
            this.traceRoute.Click += new System.EventHandler(this.traceRoute_Click);
            // 
            // console
            // 
            this.console.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.console, 2);
            this.console.Location = new System.Drawing.Point(3, 253);
            this.console.Name = "console";
            this.console.ReadOnly = true;
            this.console.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.console.Size = new System.Drawing.Size(538, 63);
            this.console.TabIndex = 3;
            this.console.Text = string.Empty;
            this.console.WordWrap = false;
            // 
            // Flot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 343);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Flot";
            this.Text = "Virtual Trace Route";
            this.Load += new System.EventHandler(this.Flot_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.WebBrowser browser;
        private System.Windows.Forms.TextBox address;
        private System.Windows.Forms.Button traceRoute;
        private System.Windows.Forms.RichTextBox console;
    }
}

