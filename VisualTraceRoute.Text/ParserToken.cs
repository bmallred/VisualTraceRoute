using System;

namespace VisualTraceRoute.Text
{
    /// <summary>
    /// Parser token attribute class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class ParserToken : Attribute
    {
        /// <summary>
        /// Gets or sets the token string.
        /// </summary>
        public string Token
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the ParserToken class.
        /// </summary>
        public ParserToken()
        {
            this.Token = string.Empty;
        }
    }
}
