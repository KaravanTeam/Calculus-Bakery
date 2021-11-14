namespace Model.Factory
{
    internal sealed class Cream
    {
        public Cream(int id, string equationValue)
        {
            ID = id;
            EquationValue = equationValue;
        }

        public int ID { get; }
        public string EquationValue { get; }
    }
}
