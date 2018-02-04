using System.Collections.Generic;
using Character;
using Framework.RuntimeSets;
using UnityEngine;

namespace RuntimeSets
{
    [CreateAssetMenu(menuName = "Sets/Characters set")]
    public class CharactersSet : RuntimeSet<CharacterTypeBehaviour>
    {
        private List<CharacterTypeBehaviour> items;

        protected override ICollection<CharacterTypeBehaviour> Collection => items;

        public CharacterTypeBehaviour FindByCharacterType(CharacterType type)
        {
            return items.Find(behaviour => behaviour.characterType == type);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            items = new List<CharacterTypeBehaviour>();
        }
    }
}