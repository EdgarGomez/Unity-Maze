using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private float rotationAngle = 90f;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private Transform turretWeapon;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float fieldOfView = 0.5f;
    [SerializeField] private float aimSpeed = 3f;

    private float currentRotation = 0f;
    private int rotationDirection = 1;
    private float nextFireTime;
    private bool playerInRange = false;
    private Transform player;

    private void Update()
    {
        CheckForPlayer();

        if (playerInRange && IsPlayerInFieldOfView())
        {
            Vector3 targetDirection = player.position - turretWeapon.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            turretWeapon.rotation = Quaternion.Slerp(turretWeapon.rotation, targetRotation, Time.deltaTime * aimSpeed);

            if (Time.time >= nextFireTime && IsAimingAtPlayer())
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
        else
        {
            Quaternion patrolRotation = Quaternion.Euler(0, currentRotation, 0);
            turretWeapon.localRotation = Quaternion.Slerp(turretWeapon.localRotation, patrolRotation, Time.deltaTime * aimSpeed);

            currentRotation += rotationSpeed * rotationDirection * Time.deltaTime;
            if (Mathf.Abs(currentRotation) >= rotationAngle)
            {
                rotationDirection *= -1;
            }
        }
    }

    private bool IsAimingAtPlayer()
    {
        return Vector3.Dot(turretWeapon.forward, (player.position - turretWeapon.position).normalized) > 0.95f;
    }

    private bool IsPlayerInFieldOfView()
    {
        if (player == null) return false;
        Vector3 directionToPlayer = (player.position - turretWeapon.position).normalized;
        return Vector3.Dot(turretWeapon.forward, directionToPlayer) > fieldOfView;
    }

    private void CheckForPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange);
        foreach (Collider col in colliders)
        {
            if (col.TryGetComponent<PlayerController>(out PlayerController playerController))
            {
                playerInRange = true;
                player = col.transform;
                return;
            }
        }
        playerInRange = false;
        player = null;
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, turretWeapon.position, turretWeapon.rotation);
        bullet.GetComponent<TurretBullet>().Initialize();
    }
}
