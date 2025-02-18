using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    bool isShooting = false;
    [SerializeField] float speed = 1.0f;
    private void Update()
    {
        ProcessShooting();
    }

    public void OnShoot(InputValue value)
    {
        isShooting = value.isPressed;
    }

    void ProcessShooting()
    {
        Debug.Log("Fire");
    }
}
