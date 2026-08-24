using System;
using Project.Scripts.Input;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField, Range(0, 1f)] private float smoothness = 0.1f;
        private Vector2 moveInput;
        private bool facingRight = true;
        [Header("Jump")]
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private bool isGrounded;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float groundDistance = 0.4f;
        [Header("Input")]
        [SerializeField] private InputReader inputReader;
        private Rigidbody rb;

        [SerializeField] private GameObject TreePrefab;

        private void OnEnable()
        {
            inputReader.Initialize(); //TODO: Ponerlo luego en un manager
            inputReader.Move += OnMove;
            inputReader.Jump += OnJump;
            inputReader.Attack += OnAttack;

        }

        private void OnDisable()
        {
            inputReader.Move -= OnMove;
            inputReader.Jump -= OnJump;
            inputReader.Attack -= OnAttack;
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (moveInput.x > 0 && !facingRight || moveInput.x < 0 && facingRight)
            {
                Flip();
            }
        }

        private void FixedUpdate()
        {
            CheckGrounded();
            // rb.linearVelocity = new Vector2(moveInput.x *  moveSpeed, rb.linearVelocity.y);
            Vector2 targetVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, smoothness);
        }

        private void CheckGrounded()
        {
            isGrounded = Physics.Raycast(transform.position, Vector3.down, groundDistance, groundLayer);
        }
        private void OnMove(Vector2 direction)
        {
            moveInput = direction;
        }
        private void OnJump()
        {
            if (isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
        private void Flip()
        {
            facingRight = !facingRight;
            var scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Vector3.down * groundDistance);
        }

        private void OnAttack()
        {
            //TODO verificar si estamos en modo árbol y si tenemos semillas disponibles
            if (isGrounded)
            {
                GameObject instance = Instantiate(TreePrefab, transform.position, Quaternion.identity);
            }
            else
            {
                GameObject.FindAnyObjectByType<Tree>()?.OnFuture();
            }
        }
    }
}
