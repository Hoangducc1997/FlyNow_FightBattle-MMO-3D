using UnityEngine;
public enum ItemType
{
    Health,
    Passive,
    UpgradeLazer,
    Victory
}
[CreateAssetMenu(fileName = "New Item", menuName = "Item System/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public GameObject itemPrefab;
    public int healAmount;
    public int passiveAmount;

    public void UseItem(GameObject target)
    {
        switch (itemType)
        {
            case ItemType.Health:
                Debug.Log("Tăng máu!");
                PlayerInfo playerHealth = target.GetComponent<PlayerInfo>();
                if (playerHealth != null)
                {
                    playerHealth.Heal(healAmount);
                    Debug.Log($"🔋 Hồi {healAmount} máu cho Player!");
                }
                break;

            case ItemType.Passive:
                Debug.Log("Tăng nội năng!");
                PlayerInfo playerPassive = target.GetComponent<PlayerInfo>();
                if (playerPassive != null)
                {
                    playerPassive.GainPassive(passiveAmount);
                    Debug.Log($"🔋 Hồi {healAmount} máu cho Player!");
                }
                break;

            case ItemType.UpgradeLazer:
                Debug.Log("Nâng cấp Lazer!");
                PlayerWeapon playerWeapon = target.GetComponent<PlayerWeapon>();
                if (playerWeapon != null)
                {
                    playerWeapon.UpgradeLevelLazer(); // Gọi nâng cấp laser
                }
                break;

            case ItemType.Victory:
                Debug.Log("Chiến thắng!");
                GameOverManager gameOverManager = GameObject.FindObjectOfType<GameOverManager>();
                if (gameOverManager != null)
                {
                    gameOverManager.Victory();
                }
                else
                {
                    Debug.LogWarning("Không tìm thấy GameOverManager!");
                }
                break;

        }
    }
}
