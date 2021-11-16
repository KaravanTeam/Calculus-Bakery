namespace Model.Transporter
{
    internal sealed class Cream
    {
        public Cream(int id, EquationType type)
        {
            ID = id;
            EquationType = type;
        }

        public int ID { get; }
        public EquationType EquationType { get; }
    }
}
