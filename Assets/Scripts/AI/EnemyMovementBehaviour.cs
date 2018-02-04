using System;
using Stats;
using UnityEngine;

namespace AI
{
    public class EnemyMovementBehaviour : MonoBehaviour, IPlayerDetectedListener, IPlayerMissedListener
    {
        public MovementStats movementStats;

        private bool isFacingRight = true;

        private GameObject trackedPlayer;

        void IPlayerDetectedListener.OnPlayerDetected(GameObject player)
        {
            trackedPlayer = player;
        }

        private void Update()
        {
            TrackPlayer();
        }

        private void TrackPlayer()
        {
            if (trackedPlayer == null) {
                return;
            }

            var playerPosition = trackedPlayer.transform.position;
            if (playerPosition.x < transform.position.x && isFacingRight) {
                Flip();
            } else if (playerPosition.x > transform.position.x && !isFacingRight) {
                Flip();
            }
        }

        private void Flip()
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(Vector3.up, 180f);
        }

        void IPlayerMissedListener.OnPlayerMissed()
        {
            trackedPlayer = null;
        }
    }
}