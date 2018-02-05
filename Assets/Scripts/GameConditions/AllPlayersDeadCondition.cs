using System.Linq;
using RuntimeSets;
using UnityEngine;

namespace GameConditions
{
    [CreateAssetMenu(menuName = "Game conditions/Players dead")]
    public class AllPlayersDeadCondition : GameCondition
    {
        public CharactersSet allCharacters;
        
        public override void Trigger()
        {
            if (hasChangedState) {
                return;
            }
            
            if (allCharacters.All(ch => !ch.IsAvailableForSwitch)) {
                IsSatisfied = false;
                OnStateChanged();
            }
        }
    }
}