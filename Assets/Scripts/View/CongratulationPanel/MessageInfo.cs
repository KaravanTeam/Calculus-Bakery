namespace View
{
    internal sealed class MessageInfo
    {
        public MessageInfo(string text, int points, MessageType type)
        {
            Text = text;
            Points = points;
            Type = type;
        }

        public string Text { get; }
        public int Points { get; }
        public MessageType Type { get; }
    }
}
