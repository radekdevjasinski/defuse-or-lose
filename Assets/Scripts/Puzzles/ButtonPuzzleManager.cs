using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DefuseOrLose
{
    public class ButtonPuzzleManager : PuzzleBase
    {
        [SerializeField] private List<Sprite> buttonSprites;

        void Start()
        {
            Initialize();
        }

        public override void Initialize()
        {
            ButtonType buttonType = ButtonPuzzleFactory.PickRandomButtonType();
            GetComponent<Button>().image.sprite = buttonSprites[(int)buttonType];
            ButtonPuzzleFactory.AttachPuzzle(buttonType, gameObject);
        }

        public void SubmitAnswer(bool isCorrect)
        {
            if (isCorrect)
            {
                OnComplete();
            }
            else
            {
                OnFail();
            }
        }
    }
}
