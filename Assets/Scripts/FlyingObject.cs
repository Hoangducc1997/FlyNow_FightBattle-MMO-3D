using UnityEngine;

public class FlyingObject : MonoBehaviour
{
    [SerializeField] float speedFly = 1.0f;
    [SerializeField] Transform player;

    Vector3 playerPosition;

    void Start()
    {
        playerPosition = player.transform.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerPosition, speedFly * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Chạm vào Terrain (Collision)");
            Destroy(gameObject, 0.5f);
        }
    }

}
