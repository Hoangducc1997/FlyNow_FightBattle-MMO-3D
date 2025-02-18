using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    bool isShooting = false;

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
