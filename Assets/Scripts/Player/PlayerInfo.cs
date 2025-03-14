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
     
    }

    //Health
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
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
