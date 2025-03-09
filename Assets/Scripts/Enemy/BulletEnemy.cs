using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField] private int damage = 10; // Sát thương của đạn
    [SerializeField] private float destroyTime = 5f; // Tự hủy sau 5 giây
    private Vector3 direction; // Lưu hướng bắn
    private float bulletSpeed; // Tốc độ đạn

    public void SetTarget(Vector3 targetPosition, float speed)
    {
        direction = (targetPosition - transform.position).normalized; // Tính hướng ngay khi bắn
        bulletSpeed = speed; // Lưu tốc độ
    }

    void Start()
    {
        Destroy(gameObject, destroyTime); // Hủy viên đạn sau thời gian cố định
    }

    void Update()
    {
        transform.position += direction * bulletSpeed * Time.deltaTime; // Di chuyển theo hướng đã tính
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInfo playerHealth = other.GetComponent<PlayerInfo>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject); // Hủy viên đạn khi trúng Player
        }
        else if (other.CompareTag("Ground"))
        {
            Destroy(gameObject); // Hủy viên đạn khi trúng tường/mặt đất
        }
    }
}
