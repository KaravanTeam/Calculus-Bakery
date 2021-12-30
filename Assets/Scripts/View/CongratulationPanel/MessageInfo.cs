namespace View
{
    internal sealed class MessageInfo
    {
        public MessageInfo(string text, int achievementPoints)
        {
            Text = text;
            AchievementPoints = achievementPoints;
        }

        public string Text { get; }
        public int AchievementPoints { get; }
    }
}
