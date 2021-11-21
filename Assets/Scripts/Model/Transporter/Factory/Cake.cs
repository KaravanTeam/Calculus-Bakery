namespace Model.Transporter
{
    internal sealed class Cake
    {
        public Cake(Bread equation, Cream equationType)
        {
            Bread = equation;
            Cream = equationType;
        }

        public Bread Bread { get; }
        public Cream Cream { get; }
    }
}
