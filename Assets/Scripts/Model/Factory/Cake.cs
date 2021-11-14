namespace Model.Factory
{
    internal sealed class Cake
    {
        public Cake(Bread equation, Cream equationType)
        {
            Equation = equation;
            EquationType = equationType;
        }

        public Bread Equation { get; }
        public Cream EquationType { get; }
    }
}
