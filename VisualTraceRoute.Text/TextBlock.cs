using System.Collections.ObjectModel;

namespace VisualTraceRoute.Text
{
    /// <summary>
    /// Text block class.
    /// </summary>
    public class TextBlock
    {
        private string _postText;
        private string _text;
        private BlockType _type;
        private Collection<TextBlock> _innerBlocks;

        /// <summary>
        /// Gets a collection of inner text blocks.
        /// </summary>
        public Collection<TextBlock> InnerBlocks
        {
            get { return this._innerBlocks; }
        }

        /// <summary>
        /// Gets or sets the separating string between iterations.
        /// </summary>
        public string PostText
        {
            get { return this._postText; }
            set { this._postText = value; }
        }

        /// <summary>
        /// Gets or sets the text block contents.
        /// </summary>
        public string Text
        {
            get { return this._text; }
            set { this._text = value; }
        }

        /// <summary>
        /// Gets or sets the text block type.
        /// </summary>
        public BlockType Type
        {
            get { return this._type; }
            set { this._type = value; }
        }

        /// <summary>
        /// Initializes a new instance of the TextBlock class.
        /// </summary>
        /// <param name="Text">Text string.</param>
        /// <param name="Type">Block type.</param>
        /// <param name="PostText">The separating string between iterations.</param>
        public TextBlock(string Text, BlockType Type = BlockType.Plain, string PostText = "")
        {
            this._innerBlocks = new Collection<TextBlock>();
            this._text = Text;
            this._type = Type;
            this._postText = PostText;
        }
    }
}
