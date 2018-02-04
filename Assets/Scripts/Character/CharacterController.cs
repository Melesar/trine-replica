using System.Text;
using Framework.Input.Data;
using Framework.References;
using Interfaces;
using UnityEngine;

namespace Character
{
    public class CharacterController : MonoBehaviour
    {
        public FloatReference speed;
        public FloatReference jumpForce;

        [Header ("Ground check")]

        public Transform groundCheck;
        public FloatReference groundCheckRadius;
        public LayerMask groundMask;

        [Header ("Animator parameters names")]

        public StringReference moveParameterName;
        public StringReference groundedParameterName;

        [Header ("Input axis names")]

        public StringReference horizontalAxisName;
        public StringReference verticalAxisName;
        public StringReference jumpAxisName;

        private Rigidbody2D rigidbody;
        private Animator animator;
        private Collider2D[] groundCheckResults = new Collider2D[1];

        private bool isGrounded;
        private bool isFacingRight = true;

        public void Move (float move, bool jump)
        {
            var velocity = move * speed.Value;
            rigidbody.velocity = new Vector2 (velocity, rigidbody.velocity.y);

            animator.SetFloat(moveParameterName, Mathf.Abs(velocity));

            if (!isFacingRight && move > 0) {
                Flip();
            } else if (isFacingRight && move < 0) {
                Flip();
            }

            if (isGrounded && jump) {
                isGrounded = false;
                rigidbody.AddForce(new Vector2(0, jumpForce));
                animator.SetBool(groundedParameterName, isGrounded);
            }
        }

        private void Flip()
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(Vector3.up, 180f);
        }

        private void FixedUpdate ()
        {
            var groundHits = Physics2D.OverlapCircleNonAlloc (groundCheck.position, groundCheckRadius, groundCheckResults, groundMask);
            isGrounded = groundHits > 0;
            animator.SetBool(groundedParameterName, isGrounded);
        }

        private void Awake ()
        {
            rigidbody = GetComponent<Rigidbody2D> ();
            animator = GetComponent<Animator>();
        }
    }
}