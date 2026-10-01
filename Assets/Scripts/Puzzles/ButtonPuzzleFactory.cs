using System;
using UnityEngine;

namespace DefuseOrLose
{
    public static class ButtonPuzzleFactory
    {
        public static ButtonType PickRandomButtonType()
        {
            Array buttonTypes = Enum.GetValues(typeof(ButtonType));
            return (ButtonType)buttonTypes.GetValue(UnityEngine.Random.Range(0, buttonTypes.Length));
        }

        public static void AttachPuzzle(ButtonType buttonType, GameObject buttonObject)
        {
            switch (buttonType)
            {
                case ButtonType.Play:
                    buttonObject.AddComponent<ButtonPuzzlePlay>();
                    break;
                case ButtonType.Stop:
                    buttonObject.AddComponent<ButtonPuzzleStop>();
                    break;
                case ButtonType.Record:
                    buttonObject.AddComponent<ButtonPuzzleRecord>();
                    break;
                case ButtonType.Heart:
                    buttonObject.AddComponent<ButtonPuzzleHeart>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(buttonType), buttonType, "Unknown button type.");
            }
        }
    }
}
