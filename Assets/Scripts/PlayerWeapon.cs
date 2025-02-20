using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    bool isShooting = false;
    [SerializeField] GameObject[] lazers;
    [SerializeField] RectTransform crossHair;
    [SerializeField] Transform targetPoint;
    [SerializeField] float targetDistance = 10f;

    private void Start()
    {
        //Cursor.visible = false;
    }
    private void Update()
    {
        ProcessShooting();
        MoveCrossHair();
        MoveTargetPoint();
        AimLazers();
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
    void MoveCrossHair() // UI 
    {
        crossHair.position = Input.mousePosition;
    }  
    void MoveTargetPoint()
    {
        Vector3 targetPointPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetDistance); ;
        targetPoint.position = Camera.main.ScreenToWorldPoint(targetPointPosition);
    }

    void AimLazers()
    {
        foreach (GameObject lazer in lazers)
        {
            Vector3 shootDirection = targetPoint.position - this.transform.position;
            Quaternion rotationToTarget = Quaternion.LookRotation(shootDirection);
            lazer.transform.rotation = rotationToTarget;
        }
    }
}
