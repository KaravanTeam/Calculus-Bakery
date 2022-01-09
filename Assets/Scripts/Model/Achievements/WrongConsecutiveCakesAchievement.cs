namespace Model.Achievements
{
    internal sealed class WrongConsecutiveCakesAchievement : CorrectСonsecutiveСakes
    {
        protected override void Subscribe()
        {
            _chef.OnCorrectCakeChecked += Reset;
            _chef.OnWrongCakeChecked += UpdateState;
        }

        protected override void Unsubcribe()
        {
            _chef.OnCorrectCakeChecked -= Reset;
            _chef.OnWrongCakeChecked -= UpdateState;
        }
    }
}
