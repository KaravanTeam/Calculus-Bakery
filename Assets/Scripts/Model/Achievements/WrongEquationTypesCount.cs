namespace Model.Achievements
{
    internal class WrongEquationTypesCount : CorrectEquationTypes
    {
        protected override void Subscribe()
        {
            _chef.OnWrongCakeChecked += UpdateState;
        }

        protected override void Unsubscribe()
        {
            _chef.OnWrongCakeChecked -= UpdateState;
        }
    }
}
