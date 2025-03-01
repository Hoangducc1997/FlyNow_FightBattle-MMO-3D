using UnityEngine;
public enum ItemType
{
    Health,
    Passive,
    UpgradeLazer
}
[CreateAssetMenu(fileName = "New Item", menuName = "Item System/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public GameObject itemPrefab;

    public void UseItem()
    {
        switch (itemType)
        {
            case ItemType.Health:
                Debug.Log("Tăng máu!");
                break;
            case ItemType.Passive:
                Debug.Log("Nhận Passive Item!");
                break;
            case ItemType.UpgradeLazer:
                Debug.Log("Nâng cấp Lazer!");
                break;
        }
    }
}
