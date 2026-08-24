using System;
using UnityEngine;

namespace Project.Scripts.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float smoothing = 5f;
        private Vector3 offset;

        private void Start()
        {
            offset = transform.position - target.position;
        }

        private void FixedUpdate()
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothing);
        }
    }
}