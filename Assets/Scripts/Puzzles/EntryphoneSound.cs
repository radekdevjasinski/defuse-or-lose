using UnityEngine;

namespace DefuseOrLose
{
    public class EntryphoneSound
    {
        public AudioClip Clip { get; }
        public int StartingNumber { get; }
        public ArithmeticOperation FirstOperation { get; }
        public ArithmeticOperation SecondOperation { get; }

        public EntryphoneSound(AudioClip clip, int startingNumber, ArithmeticOperation firstOperation, ArithmeticOperation secondOperation)
        {
            Clip = clip;
            StartingNumber = startingNumber;
            FirstOperation = firstOperation;
            SecondOperation = secondOperation;
        }
    }
}
