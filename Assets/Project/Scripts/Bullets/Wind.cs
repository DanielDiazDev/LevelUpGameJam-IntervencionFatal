using System;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private float maxTimer;
        private float timer;
        private Coroutine liftCoroutine;
        private Dictionary<PlayerController, float> playerMaxHeights = new Dictionary<PlayerController, float>();

        private void Start()
        {
            timer = maxTimer;
        }

        private void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && other.TryGetComponent(out PlayerController playerController))
            {
                float targetHeight;
                if (!playerMaxHeights.ContainsKey(playerController))
                {
                    float playerHeight = other.bounds.size.y;
                    //  targetHeight = other.transform.position.y + playerHeight * playerHeightMultiplier;
                    targetHeight = transform.position.y + playerHeight * playerHeightMultiplier;
                    playerMaxHeights[playerController] = targetHeight;
                }
                targetHeight = playerMaxHeights[playerController];
                liftCoroutine = StartCoroutine(LiftPlayer(playerController, targetHeight));
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

                // transform.position = new Vector3(
                //     transform.position.x,
                //     player.transform.position.y,
                //     transform.position.z
                // );

                yield return null;
            }
        }
        private void OnTriggerExit(Collider other)
        {

            if (other.CompareTag("Player") && other.TryGetComponent(out PlayerController playerController))
            {
                StopCoroutine(liftCoroutine);
                playerController.Rb.linearVelocity = Vector3.zero;
            }
        }
    }
}