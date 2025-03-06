using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            itemData.UseItem(other.gameObject); // Truyền đối tượng Player vào
            Destroy(gameObject);
        }
    }
}
