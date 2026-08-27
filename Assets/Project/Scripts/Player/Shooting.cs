using System;
using System.Collections.Generic;
using Project.Scripts.Bullets;
using Project.Scripts.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Scripts.Player
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] private Transform crosshair;
        [SerializeField] private float radiusCrosshair = 2f;
        [SerializeField] private float radiusShootOrigin = 1f;
        [SerializeField] private GameObject projectile;
        [SerializeField] private Transform projectileOrigin;
        [SerializeField] private float timeBetweenShots = 1.5f;
        [SerializeField] private InputReader inputReader;
        [SerializeField] private List<GameObject> projectilePrefabs;
        [SerializeField] private Transform projectileContainer;
        [Header("Animator")]
        [SerializeField] private Animator animator;
        private int currentProjectileIndex = 0;
        private bool canFire;
        private float timer;
        private UnityEngine.Camera camera;
        private Vector3 mousePos;

        private void OnEnable()
        {
            inputReader.Attack += OnShoot;
        }

        private void OnDisable()
        {
            inputReader.Attack -= OnShoot;
        }

       

        private void Awake()
        {
            camera = UnityEngine.Camera.main;
            
        }

        private void Update()
        {
            UpdateCrossHairRotation();

            if (!canFire)
            {
                timer += Time.deltaTime;
                if (timer >= timeBetweenShots)
                {
                    canFire = true;
                    timer = 0;
                }
            }
            ChangeWeapon();
        }

        private void ChangeWeapon()
        {
            if (Keyboard.current.digit1Key.wasPressedThisFrame) currentProjectileIndex = 0; //Fuego
            if (Keyboard.current.digit2Key.wasPressedThisFrame) currentProjectileIndex = 1; //Viento
            if (Keyboard.current.digit3Key.wasPressedThisFrame) currentProjectileIndex = 2; //Hielo
            if (Keyboard.current.digit4Key.wasPressedThisFrame) currentProjectileIndex = 3; //Semilla
        }

        private void UpdateCrossHairRotation()
        {
            mousePos = camera.ScreenToWorldPoint(
                new Vector3(Mouse.current.position.ReadValue().x,
                    Mouse.current.position.ReadValue().y,
                    camera.WorldToScreenPoint(transform.position).z));
            var direction = (mousePos - transform.position).normalized;
            crosshair.position = transform.position + direction * radiusCrosshair;
            var rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            crosshair.rotation = Quaternion.Euler(0, 0, rotZ);
        }
        private void OnShoot()
        {
            if (canFire)
            {
                canFire = false;
                var direction = (mousePos - transform.position).normalized;
                projectileOrigin.position = transform.position + direction * radiusShootOrigin;
                var rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                projectileOrigin.rotation = Quaternion.Euler(0, 0, rotZ);
                var projObj = Instantiate(projectilePrefabs[currentProjectileIndex], projectileContainer);
                var proj = projObj.GetComponent<ProjectileBase>();
                
                proj.Initialize(currentProjectileIndex is 0 or 2 ? 
                    projectileOrigin.position : transform.position, direction);
                
            }
        }
    }
}