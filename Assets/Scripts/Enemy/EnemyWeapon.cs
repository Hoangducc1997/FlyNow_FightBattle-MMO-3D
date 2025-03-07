using System.Collections;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;  // Prefab viên đạn
    [SerializeField] private Transform firePoint;      // Vị trí bắn
    [SerializeField] private float fireRate = 1.5f;    // Thời gian hồi bắn
    [SerializeField] private float bulletSpeed = 5f;   // Tốc độ đạn

    private Transform player;
    private bool isShooting = false;
    private Coroutine shootingCoroutine;

    void Update()
    {
        if (isShooting && player != null && shootingCoroutine == null)
        {
            shootingCoroutine = StartCoroutine(ShootCoroutine());
        }
    }

    private IEnumerator ShootCoroutine()
    {
        while (isShooting)
        {
            Shoot();
            yield return new WaitForSeconds(fireRate); // Kiểm soát tốc độ bắn
        }
        shootingCoroutine = null; // Reset coroutine khi dừng bắn
    }

    private void Shoot()
    {
        if (firePoint == null || bulletPrefab == null)
        {
            Debug.LogError("❌ Thiếu firePoint hoặc bulletPrefab!");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        BulletEnemy bulletScript = bullet.GetComponent<BulletEnemy>();

        if (bulletScript != null && player != null)
        {
            Vector3 targetPosition = player.position; // Lấy vị trí player tại thời điểm bắn
            bulletScript.SetTarget(targetPosition, bulletSpeed); // Truyền vị trí cố định lúc bắn
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            isShooting = true;
            Debug.Log("✅ Player vào vùng bắn!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isShooting = false;
            player = null;
            Debug.Log("🚪 Player rời khỏi vùng bắn!");

            // Dừng coroutine khi Player rời đi
            if (shootingCoroutine != null)
            {
                StopCoroutine(shootingCoroutine);
                shootingCoroutine = null;
            }
        }
    }
}
