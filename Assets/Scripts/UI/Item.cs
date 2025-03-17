using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData;
    private Transform player;
    private bool moveToPlayer = false;
    public float moveSpeed = 5f; // Tốc độ di chuyển về Player

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player != null)
        {
            moveToPlayer = true; // Kích hoạt di chuyển về Player
        }
    }

    private void Update()
    {
        if (moveToPlayer && player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            itemData.UseItem(other.gameObject);
            AudioManager.Instance.PlayVFX("Item Pickup");
            Destroy(gameObject);
        }
    }
}
