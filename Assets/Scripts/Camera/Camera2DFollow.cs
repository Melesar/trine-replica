using System;
using UnityEngine;
using Variables;
using RuntimeSets;
using Character;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public BoundsVariable followBounds;
        public CharacterTypeVariable currentCharacter;
        public CharactersSet characters;

        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;

        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;

        private Transform target;
        private Camera cam;

        // Use this for initialization
        private void Awake()
        {
            cam = GetComponent<Camera>();
            currentCharacter.valueChanged += OnCurrentCharacterChanged;
        }

        // Update is called once per frame
        private void Update()
        {
            if (target == null) {
                return;
            }
            
            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target.position - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward*m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);
            
            newPos.x = Mathf.Clamp(newPos.x,
                followBounds.Value.center.x - followBounds.Value.extents.x + cam.aspect * cam.orthographicSize,
                followBounds.Value.center.x + followBounds.Value.extents.x - cam.aspect * cam.orthographicSize);
            newPos.y = Mathf.Clamp(newPos.y,
                followBounds.Value.center.y - followBounds.Value.extents.y + cam.orthographicSize,
                followBounds.Value.center.y + followBounds.Value.extents.y - cam.orthographicSize);

            transform.position = newPos;

            m_LastTargetPosition = target.position;
        }

        private void OnDestroy()
        {
            currentCharacter.valueChanged -= OnCurrentCharacterChanged;
        }

        private void OnCurrentCharacterChanged(CharacterType oldCharacterType, CharacterType newCharacterType)
        {
            target = characters.FindByCharacterType(newCharacterType).transform;

            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
            transform.parent = null;
        }
    }
}
