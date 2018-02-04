using UnityEngine;

namespace Character
{
    [CreateAssetMenu(menuName = "Character type")]
    public class CharacterType : ScriptableObject
    {
        public bool activeByDefault;
    }
}