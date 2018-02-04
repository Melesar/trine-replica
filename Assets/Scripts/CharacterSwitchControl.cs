using UnityEngine;
using System.Collections.Generic;
using Character;

public class CharacterSwitchControl : MonoBehaviour
{
    public List<CharacterTypeKeybinding> keyBindings;

    private CharacterSwitch characterSwitch;
    private Dictionary<KeyCode, CharacterType> keyBindingsMap;

    private void Update()
    {
        if (!Input.anyKeyDown) {
            return;
        }

        foreach (var key in keyBindingsMap.Keys) {
            if (Input.GetKeyDown(key)) {
                characterSwitch.SwitchToCharacter(keyBindingsMap[key]);
                return;
            }
        }
    }

    private void Start()
    {
        foreach (var binding in keyBindings) {
            keyBindingsMap.Add(binding.key, binding.characterType);
        }
    }

    private void Awake()
    {
        characterSwitch = GetComponent<CharacterSwitch>();
        keyBindingsMap = new Dictionary<KeyCode, CharacterType>();
    }
}

[System.Serializable]
public struct CharacterTypeKeybinding
{
    public KeyCode key;
    public CharacterType characterType;
}