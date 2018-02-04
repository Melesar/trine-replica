using System.Collections.Generic;
using System.Linq;
using Framework.EventListeners;
using Framework.Events;
using RuntimeSets;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(menuName = "Events/Game over event")]
    public class GameOverEvent : GameEvent, IEventListener<GameObject>
    {
        public GameObjectEvent playerDeadEvent;
        public CharactersSet allCharacters;

        private HashSet<GameObject> playersLeft;
        
        public void OnEventRaised(GameObject arg)
        {
            if (allCharacters.All(ch => !ch.IsAvailableForSwitch)) {
                Raise();
            }
        }

        private void OnEnable()
        {
            playerDeadEvent.AddListener(this);
            playersLeft = new HashSet<GameObject>(allCharacters.Select(ch => ch.gameObject));
        }

        private void OnDestroy()
        {
            playerDeadEvent.RemoveListener(this);
        }
    }
}