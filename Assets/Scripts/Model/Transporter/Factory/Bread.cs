namespace Model.Transporter
{
    internal sealed class Bread
    {
        public Bread(int id, string value)
        {
            ID = id;
            Value = value;
        }

        public int ID { get; }
        public string Value { get; }
    }
}
