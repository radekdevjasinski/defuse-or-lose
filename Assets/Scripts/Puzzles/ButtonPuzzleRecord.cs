namespace DefuseOrLose
{
    public class ButtonPuzzleRecord : ButtonPuzzle
    {
        private const char AnswerDigit = '4';

        protected override void ExecutePuzzle()
        {
            bool isAnswerDigitOnTimer = BombController.Instance.FormattedTime.IndexOf(AnswerDigit) >= 0;
            buttonPuzzleManager.SubmitAnswer(isAnswerDigitOnTimer);
        }
    }
}
