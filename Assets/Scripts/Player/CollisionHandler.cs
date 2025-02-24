using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Hit{other.gameObject.name}");
        //Debug.Log("Hit " + other.name);
    }
}
