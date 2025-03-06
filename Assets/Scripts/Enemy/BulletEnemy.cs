using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField] int damage = 10; // Sát thương của đạn
    [SerializeField] float destroyTime = 3f; // Tự hủy sau 3 giây

    void Start()
    {
        Destroy(gameObject, destroyTime); // Hủy viên đạn sau một khoảng thời gian
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInfo playerHealth = other.GetComponent<PlayerInfo>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log($"💥 Đạn trúng Player! Gây {damage} sát thương.");
            }
            else
            {
                Debug.LogWarning("⚠️ Không tìm thấy PlayerHealth trên Player!");
            }

            Destroy(gameObject); // Hủy viên đạn sau khi trúng Player
        }
        else if (other.CompareTag("Ground"))
        {
            Destroy(gameObject); // Hủy đạn khi trúng tường hoặc mặt đất
        }
    }
}
