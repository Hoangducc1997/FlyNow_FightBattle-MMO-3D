using JetBrains.Annotations;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject destroyedVFX;
    [SerializeField] int hitPoints = 2;
    [SerializeField] int score = 0;

 
    Score scoreIncrease;

    [SerializeField] private ItemData[] possibleItemDrops; // Danh sách item có thể rơi
    [SerializeField] private float dropRate = 0.5f; // Xác suất rơi item

    private void Start()
    {
        scoreIncrease = FindFirstObjectByType<Score>();
    }
    private void OnParticleCollision(GameObject other)
    {
        hitPoints --;
        if (hitPoints <= 0)
        {
            Instantiate(destroyedVFX, transform.position, Quaternion.identity);
            SpawnItem();
            Destroy(this.gameObject);
            scoreIncrease.IncreaseScore(score);
        }
    }
    private void SpawnItem()
    {
        if (possibleItemDrops.Length > 0 && Random.value < dropRate)
        {
            int randomIndex = Random.Range(0, possibleItemDrops.Length);
            ItemData selectedItem = possibleItemDrops[randomIndex];

            Debug.Log($"Item được chọn: {selectedItem.itemName}");

            if (selectedItem.itemPrefab != null)
            {
                Instantiate(selectedItem.itemPrefab, transform.position, Quaternion.identity);
                Debug.Log("Item đã spawn!");
            }
            else
            {
                Debug.LogWarning($"Item {selectedItem.itemName} không có Prefab!");
            }
        }
        else
        {
            Debug.Log("Không spawn item (do tỉ lệ rơi hoặc danh sách rỗng).");
        }
    }



}
