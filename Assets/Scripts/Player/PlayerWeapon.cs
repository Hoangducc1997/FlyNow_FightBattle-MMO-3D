using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

[System.Serializable] // Bắt buộc để hiển thị trong Inspector
public class LazerLevel
{
    public GameObject[] lazerObjects; // Mảng GameObject cho từng cấp
}

public class PlayerWeapon : MonoBehaviour
{
    bool isShooting = false;
    bool isSpecialSkill = false;

    [SerializeField] List<LazerLevel> levelLazers = new List<LazerLevel>(); // Chứa nhiều cấp độ, mỗi cấp có nhiều Lazer
    [SerializeField] RectTransform crossHair;
    [SerializeField] Transform targetPoint;
    [SerializeField] float targetDistance = 10f;
    [SerializeField] GameObject specialSkill;
    [SerializeField] PlayerInfo playerInfo; // Tham chiếu đến PlayerInfo

    private int currentLevel = 0; // Cấp độ hiện tại

    private void Start()
    {
        // Tắt tất cả Lazer, chỉ bật cấp 0
        for (int i = 0; i < levelLazers.Count; i++)
        {
            SetActiveLazer(i, i == 0);
        }
        specialSkill.SetActive(false);
    }

    private void Update()
    {
        ProcessShooting();
        ProcessSpecialSkill();
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
        isSpecialSkill = value.isPressed;
    }

    void ProcessShooting()
    {
        foreach (var lazer in levelLazers[currentLevel].lazerObjects) // Chỉ bắn Lazer của cấp hiện tại
        {
            var emissionModule = lazer.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isShooting;
        }
    }

    void ProcessSpecialSkill()
    {
        if (isSpecialSkill && playerInfo.GetCurrentPassive() == 100)
        {
            specialSkill.SetActive(true);
            playerInfo.ResetPassive(); // Reset thanh Passive về 0
            Debug.Log("🔥 Kích hoạt kỹ năng đặc biệt!");
        }
    }

    void MoveCrossHair()
    {
        crossHair.position = Input.mousePosition;
    }

    void MoveTargetPoint()
    {
        Vector3 targetPointPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetDistance);
        targetPoint.position = Camera.main.ScreenToWorldPoint(targetPointPosition);
    }

    void AimLazers()
    {
        foreach (var lazer in levelLazers[currentLevel].lazerObjects) // Chỉ xoay Lazer của cấp hiện tại
        {
            Vector3 shootDirection = targetPoint.position - this.transform.position;
            Quaternion rotationToTarget = Quaternion.LookRotation(shootDirection);
            lazer.transform.rotation = rotationToTarget;
        }
    }

    public void UpgradeLevelLazer()
    {
        if (currentLevel < levelLazers.Count - 1) // Kiểm tra nếu chưa đạt max level
        {
            SetActiveLazer(currentLevel, false); // Tắt toàn bộ Lazer cấp hiện tại
            currentLevel++; // Tăng cấp độ
            SetActiveLazer(currentLevel, true); // Bật toàn bộ Lazer cấp mới

            Debug.Log($"🔺 Nâng cấp Laser lên Level {currentLevel + 1}");
        }
        else
        {
            Debug.Log("⚠ Laser đã đạt cấp độ tối đa!");
        }
    }

    private void SetActiveLazer(int levelIndex, bool isActive)
    {
        if (levelIndex >= 0 && levelIndex < levelLazers.Count)
        {
            foreach (GameObject lazer in levelLazers[levelIndex].lazerObjects)
            {
                if (lazer != null)
                    lazer.SetActive(isActive);
            }
        }
    }
}
