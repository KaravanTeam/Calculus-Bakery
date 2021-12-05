namespace Model.Transporter
{
    internal sealed class Solution
    {
        public Solution(int id, string value)
        {
            ID = id;
            Value = value;
        }

        public int ID { get; }
        public string Value { get; }
    }
}
