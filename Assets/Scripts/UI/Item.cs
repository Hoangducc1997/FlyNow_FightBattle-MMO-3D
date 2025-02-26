using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] ItemData itemData;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            itemData.UseItem();
            Destroy(gameObject);
        }
    }
}