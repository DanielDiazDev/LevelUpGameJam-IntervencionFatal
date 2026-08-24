using System;
using System.Collections;
using Project.Scripts.Player;
using UnityEngine;

namespace Project.Scripts.Bullets
{
    public class Wind : SpawnedProjectile
    {
        [SerializeField] private float liftForce = 5f;
        [SerializeField] private float levitateAmplitude = 0.5f;
        [SerializeField] private float levitateFrequency = 2f;
        [SerializeField] private float playerHeightMultiplier = 2f;
        private Coroutine liftCoroutine;
        private bool isActive;
        private void OnTriggerEnter(Collider other)
        {
            if (!isActive)
            {
                if (other.CompareTag("Player") && other.TryGetComponent(out PlayerController playerController))
                {
                    float playerHeight = other.bounds.size.y;
                    float targetHeight = other.transform.position.y + playerHeight * playerHeightMultiplier;

                    isActive = true;
                    liftCoroutine = StartCoroutine(LiftPlayer(playerController, targetHeight));
                }
            }
        }
       
        private IEnumerator LiftPlayer(PlayerController player, float targetHeight)
        {
            Rigidbody playerRb = player.Rb;
            while (player.transform.position.y < targetHeight)
            {
                float distance = targetHeight - player.transform.position.y;

                float verticalVelocity = Mathf.Clamp(distance * liftForce, 0f, liftForce);

                playerRb.linearVelocity = new Vector3(
                    playerRb.linearVelocity.x,
                    verticalVelocity,
                    playerRb.linearVelocity.z
                );

                transform.position = new Vector3(
                    transform.position.x,
                    player.transform.position.y,
                    transform.position.z
                );

                yield return null;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (isActive)
            {
                if (other.CompareTag("Player") && other.TryGetComponent(out PlayerController playerController))
                {
                    StopCoroutine(liftCoroutine);
                    playerController.Rb.linearVelocity = Vector3.zero;
                    isActive = false;
                    rb.linearVelocity = Vector3.zero;
                }
            }
        }
    }
}