using UnityEngine;

public class FlyingObject : MonoBehaviour
{
    [SerializeField] float speedFly = 1.0f;
    [SerializeField] Transform player;
    [SerializeField] int damageAtkPlayer = 1;

    Vector3 playerPosition;

    void Start()
    {
        if (player != null)
        {
            playerPosition = player.transform.position;
        }
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerPosition, speedFly * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Chạm vào Terrain");
            Destroy(gameObject, 0.5f);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Chạm vào Player");
            PlayerInfo playerInfo = collision.gameObject.GetComponent<PlayerInfo>();
            if (playerInfo != null)
            {
                playerInfo.TakeDamage(damageAtkPlayer);
            }           
            Destroy(gameObject, 0.5f);
        }
    }

}
