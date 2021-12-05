namespace Model.Transporter
{
    internal sealed class Equation
    {
        public Equation(int id, string value, EquationType type)
        {
            ID = id;
            Value = value;
            Type = type;
        }

        public int ID { get; }
        public string Value { get; }
        public EquationType Type { get; }
    }
}
