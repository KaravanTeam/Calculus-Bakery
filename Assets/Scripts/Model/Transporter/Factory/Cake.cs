namespace Model.Transporter
{
    internal sealed class Cake
    {
        public Cake(Equation equation, Solution solution)
        {
            Bread = equation;
            Cream = solution;
        }

        public Equation Bread { get; }
        public Solution Cream { get; }
    }
}
