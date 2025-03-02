using System.Collections;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;  // Prefab viên đạn
    [SerializeField] private Transform firePoint;      // Vị trí bắn
    [SerializeField] private float bulletSpeed = 10f;  // Tốc độ đạn
    [SerializeField] private float fireRate = 1.5f;    // Thời gian hồi đạn (giây)

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
            yield return new WaitForSeconds(fireRate); // Đợi thời gian hồi đạn
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
        Debug.Log($"🚀 Bắn bullet từ {firePoint.position}");

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("❌ Không tìm thấy Rigidbody trong bulletPrefab! Hãy kiểm tra Prefab viên đạn.");
            return;
        }

        Vector3 direction = (player.position - firePoint.position).normalized;
        rb.linearVelocity = direction * bulletSpeed; // Dùng linearVelocity thay cho velocity
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
