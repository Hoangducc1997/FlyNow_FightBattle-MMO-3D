using UnityEngine;
using UnityEngine.UIElements;

public class FlyingObject : MonoBehaviour
{
    [SerializeField] float speedFly = 1.0f;
    [SerializeField] Transform player;
    Vector3 playerPosition;
    //Va chạm với player sẽ làm player mất máu
    void Start()
    {
        playerPosition = player.transform.position;
    }

    void Update()
    {
        transform.position =
            Vector3.MoveTowards(transform.position, playerPosition, speedFly * Time.deltaTime);

    }
}
