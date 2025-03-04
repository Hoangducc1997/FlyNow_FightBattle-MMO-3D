using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    bool isShooting = false;
    [SerializeField] GameObject[] lazers;
    [SerializeField] RectTransform crossHair;
    [SerializeField] Transform targetPoint;
    [SerializeField] float targetDistance = 10f;
    [SerializeField] public ParticleSystem pfx;

    [SerializeField] public Bullet bulletPrefab;
    [SerializeField] public float speedBullet = 10;

    public Action OnDoneTutorial;

    public bool IsPlayingTutorial { get; set; }

    private void Start()
    {
        foreach (var lazer in lazers)
        {
            var emmisionModule = lazer.GetComponent<ParticleSystem>().emission;
            emmisionModule.enabled = false;
        }
    }

    private void Update()
    {
        if (IsPlayingTutorial)
            return;

        if (isShooting)
        {
            SpawnBullet();
        }

        ProcessShooting();
        MoveCrossHair();
        MoveTargetPoint();
        AimLazers();
    }

    public void OnShoot(InputValue value)
    {
        isShooting = value.isPressed;
    }

    public void OnSpecialSkill(InputValue value)
    {
        if (value.isPressed)
        {
            pfx.transform.rotation = GetQuaternion();
            pfx.Play();
            SpawnBullet();
        }
    }

    void ProcessShooting()
    {
        foreach (var lazer in lazers)
        {
            var emmisionModule = lazer.GetComponent<ParticleSystem>().emission;
            emmisionModule.enabled = isShooting;
        }
    }
    void MoveCrossHair()
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
            lazer.transform.rotation = GetQuaternion();
        }
    }

    private Quaternion GetQuaternion()
    {
        Vector3 shootDirection = targetPoint.position - this.transform.position;
        Quaternion rotationToTarget = Quaternion.LookRotation(shootDirection);

        return rotationToTarget;
    }

    private bool isLockSpawn = false;

    private Vector3 GetShootDirection()
    {
        return (targetPoint.position - transform.position).normalized;
    }

    private void SpawnBullet()
    {
        if (isLockSpawn)
            return;

        StartCoroutine(IELockSpawn());

        Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector3 shootDirection = GetShootDirection();
        bullet.GetComponent<Rigidbody>().linearVelocity = shootDirection * speedBullet;
        bullet.InitBullet(OnDoneTutorial);
    }
    private IEnumerator IELockSpawn()
    {
        isLockSpawn = true;

        yield return new WaitForSeconds(.3f);

        isLockSpawn = false;
    }
}
