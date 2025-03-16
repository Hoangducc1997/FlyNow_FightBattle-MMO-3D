using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    [Header("- Health Player -")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    [Header("- Passive Player -")]
    [SerializeField] private Slider passiveBar;
    [SerializeField] private int maxPassive = 100;

    [Header("- Explosion Effect -")]
    [SerializeField] private GameObject explosionEffect; // Hiệu ứng nổ

    private int currentPassive = 0;

    private void Start()
    {
        currentHealth = maxHealth;
        currentPassive = 0;
        UpdateHealthBar();
        UpdatePassiveBar();
    }
    private void Update()
    {
        if (currentHealth < 0)
        {
            Die(); // Gọi Game Over khi máu dưới 100
        }
    }


    // Nhận sát thương
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die(); // Gọi hàm chết khi máu về 0
        }
    }
    private void Die()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        Invoke("DestroyPlayer", 2f);

        GameOverManager gameOverManager = FindObjectOfType<GameOverManager>();
        if (gameOverManager != null)
        {
            gameOverManager.GameOverWithDelay(2.0f); // Delay 2 giây rồi hiện bảng Game Over
        }
    }
    private void DestroyPlayer()
    {
        gameObject.SetActive(false); // Ẩn Player thay vì xóa ngay

        // Hủy Player sau khi hiệu ứng kết thúc (giả sử hiệu ứng kéo dài 2s)
        Destroy(gameObject, 2f);
    }


    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }
    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = (float)currentHealth / maxHealth;
        }
    }

    //Passive
    public void GainPassive(int amount)
    {
        currentPassive += amount;
        currentPassive = Mathf.Clamp(currentPassive, 0, maxPassive);
        UpdatePassiveBar();
    }
    public void ResetPassive()
    {
        currentPassive = 0;
        UpdatePassiveBar();
    }
    public int GetCurrentPassive()
    {
        return currentPassive;
    }

    private void UpdatePassiveBar()
    {
        if (passiveBar != null)
        {
            passiveBar.value = (float)currentPassive / maxPassive;
        }
    }
}
