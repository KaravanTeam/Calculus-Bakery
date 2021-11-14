namespace Model.Factory
{
    internal sealed class Bread
    {
        public Bread(int id, EquationType equationType)
        {
            ID = id;
            EquationType = equationType;
        }

        public int ID { get; }
        public EquationType EquationType { get; }
    }
}
