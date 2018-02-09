using System;

namespace Serialization
{
    [Serializable]
    public struct LevelInfo
    {
        public int number;
        public bool isReached;
        public bool isCompleted;

        public override string ToString()
        {
            return $"{nameof(number)}: {number}, {nameof(isReached)}: {isReached}, {nameof(isCompleted)}: {isCompleted}";
        }
    }
}