using Character;
using UnityEngine.EventSystems;

namespace Interfaces
{
    public interface ICharacterChangedListener : IEventSystemHandler
    {
        void OnCharacterChanged(CharacterType thisCharacter, CharacterType newCharacter);
    }
}