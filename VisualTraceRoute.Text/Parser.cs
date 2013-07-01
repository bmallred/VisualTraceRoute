using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VisualTraceRoute.Net;

namespace VisualTraceRoute.Text
{
    /// <summary>
    /// Text parser for writing output to various data structures.
    /// </summary>
    public class Parser
    {
        private List<TextBlock> _blocks;
        private FileInfo _templateFile;

        /// <summary>
        /// Initializes a new instance of the Parser class.
        /// </summary>
        public Parser()
        {
            this._blocks = new List<TextBlock>();
            this._templateFile = null;
        }

        /// <summary>
        /// Initializes a new instance of the Parser class.
        /// </summary>
        /// <param name="TemplateFile">Template file path.</param>
        public Parser(string TemplateFile)
            : this(new FileInfo(TemplateFile))
        {
            // Stubble.
        }

        /// <summary>
        /// Initializes a new instance of the Parser class.
        /// </summary>
        /// <param name="TemplateFile">Template file.</param>
        public Parser(FileInfo TemplateFile)
        {
            this._blocks = new List<TextBlock>();
            this._templateFile = TemplateFile;
        }

        /// <summary>
        /// Read the associated template.
        /// </summary>
        public void ReadTemplate()
        {
            // Clear previous text blocks stored.
            this._blocks.Clear();

            if (this._templateFile == null)
            {
                return;
            }

            if (this._templateFile.Exists)
            {
                using (TextReader reader = new StreamReader(this._templateFile.OpenRead()))
                {
                    StringBuilder newBlock = new StringBuilder();

                    // Check if there is more content to read.
                    while (reader.Peek() >= 0)
                    {
                        // Read the latest line.
                        string line = reader.ReadLine();

                        // Determine if there are any tokens that need to be processed.
                        if (line.ToUpper().Contains("{ROUTES}"))
                        {
                            this._blocks.Add(new TextBlock(newBlock.ToString()));
                            newBlock.Clear();
                        }
                        else if (line.ToUpper().Contains("{/ROUTES}"))
                        {
                            int idx = line.IndexOf("{/ROUTES}", 0, StringComparison.CurrentCultureIgnoreCase);
                            int trashLength = idx + "{/ROUTES}".Length;
                            string postText = line.Substring(trashLength, line.Length - trashLength);

                            this._blocks.Add(new TextBlock(newBlock.ToString(), BlockType.Route, postText));
                            newBlock.Clear();
                        }
                        else if (line.ToUpper().Contains("{HOPS}"))
                        {
                            this._blocks.Add(new TextBlock(newBlock.ToString(), BlockType.Route));
                            newBlock.Clear();
                        }
                        else if (line.ToUpper().Contains("{/HOPS}"))
                        {
                            int idx = line.IndexOf("{/HOPS}", 0, StringComparison.CurrentCultureIgnoreCase);
                            int trashLength = idx + "{/HOPS}".Length;
                            string postText = line.Substring(trashLength, line.Length - trashLength);

                            this._blocks.Last().InnerBlocks.Add(new TextBlock(newBlock.ToString(), BlockType.Hop, postText));
                            newBlock.Clear();
                        }
                        else
                        {
                            newBlock.AppendLine(line);
                        }
                    }

                    // If there is anything still in the read buffer add it as a new block.
                    if (newBlock.Length > 0)
                    {
                        this._blocks.Add(new TextBlock(newBlock.ToString()));
                        newBlock.Clear();
                    }
                }
            }
        }

        /// <summary>
        /// Read the associated template.
        /// </summary>
        /// <param name="TemplateFile">Template file path.</param>
        public void ReadTemplate(string TemplateFile)
        {
            this.ReadTemplate(new FileInfo(TemplateFile));
        }

        /// <summary>
        /// Read the associated template.
        /// </summary>
        /// <param name="TemplateFile">Template file.</param>
        public void ReadTemplate(FileInfo TemplateFile)
        {
            this._templateFile = TemplateFile;
            this.ReadTemplate();
        }

        /// <summary>
        /// Write data structure to a stream.
        /// </summary>
        /// <param name="OutputStream">Output stream.</param>
        /// <param name="Routes">Array of trace route replies.</param>
        public void ToStream(Stream OutputStream, TraceRoute[] Routes)
        {
            using (StreamWriter writer = new StreamWriter(OutputStream))
            {
                foreach (TextBlock block in this._blocks)
                {
                    switch (block.Type)
                    {
                        case BlockType.Route:

                            foreach (TraceRoute route in Routes)
                            {
                                // Work with the copy dummy.
                                string line = block.Text;
                                int idx;

                                // Search for destination tokens.
                                while ((idx = line.IndexOf("{destination}", StringComparison.CurrentCultureIgnoreCase)) > -1)
                                {
                                    line = line.Replace(line.Substring(idx, "{destination}".Length), route.Destination);
                                }

                                // Finally...
                                writer.Write(line);
                                writer.Flush();

                                // Loop through any inner blocks.
                                foreach (TextBlock innerBlock in block.InnerBlocks)
                                {
                                    this.WriteHops(writer, innerBlock, route.Hops.ToArray());
                                }

                                if (!string.IsNullOrEmpty(block.PostText))
                                {
                                    // Add the separator if it is not the last route.
                                    if (route != Routes.Last())
                                    {
                                        line += block.PostText;
                                    }

                                    // Finally...
                                    writer.Write(line);
                                    writer.Flush();
                                }
                            }

                            break;

                        case BlockType.Hop:

                            // Stub.

                            break;

                        default:

                            // Nothing to see here.
                            writer.Write(block.Text);
                            writer.Flush();

                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Write data structure to a specific file.
        /// </summary>
        /// <param name="TargetFile">Target output file path.</param>
        /// <param name="Routes">Array of trace route replies.</param>
        /// <param name="Append">A value indicating whether to append data on existing file contents.</param>
        public void Write(string TargetFile, TraceRoute[] Routes, bool Append = false)
        {
            this.Write(new FileInfo(TargetFile), Routes, Append);
        }

        /// <summary>
        /// Write data structure to a specific file.
        /// </summary>
        /// <param name="TargetFile">Target output file.</param>
        /// <param name="Routes">Array of trace route replies.</param>
        /// <param name="Append">A value indicating whether to append data on existing file contents.</param>
        public void Write(FileInfo TargetFile, TraceRoute[] Routes, bool Append = false)
        {
            this.ToStream(TargetFile.Open(Append ? FileMode.Append : FileMode.Create), Routes);
        }

        private void WriteHops(StreamWriter writer, TextBlock block, Hop[] hops)
        {
            foreach (Hop hop in hops)
            {
                // Work with the copy dummy.
                string line = block.Text;
                int idx;

                // Search for address tokens.
                while ((idx = line.IndexOf("{address}", StringComparison.CurrentCultureIgnoreCase)) > -1)
                {
                    line = line.Replace(line.Substring(idx, "{address}".Length), hop.Address);
                }

                // Search for hop tokens.
                while ((idx = line.IndexOf("{hop}", StringComparison.CurrentCultureIgnoreCase)) > -1)
                {
                    line = line.Replace(line.Substring(idx, "{hop}".Length), hop.HopCount.ToString());
                }

                // Search for host name tokens.
                while ((idx = line.IndexOf("{hostname}", StringComparison.CurrentCultureIgnoreCase)) > -1)
                {
                    line = line.Replace(line.Substring(idx, "{hostname}".Length), hop.HostName);
                }

                // Search for round trip tokens.
                while ((idx = line.IndexOf("{roundtrip}", StringComparison.CurrentCultureIgnoreCase)) > -1)
                {
                    line = line.Replace(line.Substring(idx, "{roundtrip}".Length), hop.RoundTrip.ToString());
                }

                if (!string.IsNullOrEmpty(block.PostText))
                {
                    // Add the separator if it is not the last route.
                    if (hop != hops.Last())
                    {
                        line += block.PostText;
                    }
                }

                // Finally...
                writer.Write(line);
                writer.Flush();
            }
        }
    }
}
