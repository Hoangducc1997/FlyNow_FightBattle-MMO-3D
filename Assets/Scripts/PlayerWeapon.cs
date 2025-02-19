using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    bool isShooting = false;
    [SerializeField] GameObject[] lazers;

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
        foreach (var lazer in lazers) // Shoot many lazers
        {
            var emmisionModule = lazer.GetComponent<ParticleSystem>().emission;
            emmisionModule.enabled = isShooting;
        }
    }
}
