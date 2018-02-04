using System.Linq;
using UnityEngine;
using RuntimeSets;
using Character;
using Variables;

public class CharacterSwitch : MonoBehaviour
{
    public CharactersSet characters;
    public CharacterTypeVariable currentType;

    private CharacterTypeBehaviour currentCharacter;

    public void SwitchToCharacter (CharacterType newCharacterType)
    {
        if (newCharacterType == currentType) {
            return;
        }

        var newCharacter = characters.FindByCharacterType(newCharacterType);
        if (!newCharacter.IsAvailableForSwitch) {
            return;
        }
        
        currentType.Value = newCharacterType;
        currentCharacter = newCharacter;
    }

    public void OnPlayerDead(GameObject player)
    {
        SwitchToAnotherCharacter();
    }

    private void SwitchToAnotherCharacter()
    {
        var newCharacter = characters.FirstOrDefault(ch => ch.IsAvailableForSwitch);
        if (newCharacter == null) {
            //TODO Probably, all characters are dead - game over
            return;
        }

        currentType.Value = newCharacter.characterType;
        currentCharacter = newCharacter;
    }

    private void SwitchCharacter(bool isActive)
    {
        if (currentCharacter == null) {
            return;
        }

        currentCharacter.gameObject.SetActive(isActive);
    }

    private void Start()
    {
        var activeByDefault = characters.FirstOrDefault(t => t.characterType.activeByDefault);
        if (activeByDefault == null) {
            return;
        }

        currentCharacter = activeByDefault;
        currentType.Value = currentCharacter.characterType;
    }
}