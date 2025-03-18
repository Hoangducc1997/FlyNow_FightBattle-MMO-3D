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

    [SerializeField] private Animator animator; 
    private bool isSpecial = false;

    // Biến kiểm soát để tránh lặp âm thanh
    private bool hasPlayedFullPassive = false;
    private bool hasPlayedSpecial = false;

    public bool IsPlayingTutorial { get; set; }
    private void Start()
    {
        Cursor.visible = false; // Ẩn con trỏ chuột
        isSpecialSkill = false; // Đảm bảo mặc định là false
        Debug.Log("Giá trị khởi tạo của isSpecialSkill: " + isSpecialSkill);

        for (int i = 0; i < levelLazers.Count; i++)
        {
            SetActiveLazer(i, i == 0);
        }
        specialSkill.SetActive(false);
    }


    private void Update()
    {
        if (IsPlayingTutorial)
            return;
        ProcessShooting();
        ProcessSpecialSkill();
        MoveCrossHair();
        MoveTargetPoint();
        AimLazers();

        // 🛠 Xoay specialSkill theo hướng chuột liên tục
        if (specialSkill.activeSelf)
        {
            AimSpecialSkill();
        }
    }

    public void OnShoot(InputValue value)
    {
        isShooting = value.isPressed;
    }
    public void OnSpecialSkill(InputValue value)
    {
        // Nếu Passive chưa đủ 100 thì bỏ qua luôn, không cho phép nhấn F
        if (playerInfo.GetCurrentPassive() < 100)
            return;

        isSpecialSkill = value.isPressed;
    }

    void ProcessShooting()
    {
        foreach (var lazer in levelLazers[currentLevel].lazerObjects)
        {
            var emissionModule = lazer.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isShooting;
        }

        // Chỉ phát âm thanh khi đang bắn và chưa có âm thanh "Lazer" chạy
        if (isShooting && !AudioManager.Instance.IsPlayingVFX("Lazer"))
        {
            AudioManager.Instance.PlayVFX("Lazer");
        }
    }

    void ProcessSpecialSkill()
    {
        if (playerInfo.GetCurrentPassive() < 100)
        {
            hasPlayedFullPassive = false; // Reset nếu chưa đạt 100
            return;
        }

        // 🟢 Phát âm thanh "Full Passive" chỉ 1 lần
        if (playerInfo.GetCurrentPassive() == 100 && !hasPlayedFullPassive)
        {
            hasPlayedFullPassive = true; // Đánh dấu đã phát
            AudioManager.Instance.PlayVFX("Full Passive");
        }

        // 🟢 Khi nhấn F, kích hoạt animation nhưng **chưa bắn**
        if (isSpecialSkill)
        {
            isSpecialSkill = false; // Reset tránh spam F
            isSpecial = true; // Đánh dấu đang trong chế độ đặc biệt

            AudioManager.Instance.PlayVFX("Full Passive Press F");

            if (animator != null)
            {
                animator.SetBool("isSpecial", true);
            }

            Debug.Log("🔄 Passive đạt 100! Nhấn chuột trái để bắn Special Skill.");
        }

        // 🔥 Khi đã kích hoạt bằng F và nhấn chuột trái thì mới bắn
        if (isSpecial && isShooting)
        {
            isSpecial = false; // Ngăn không bắn nhiều lần khi giữ chuột
            specialSkill.SetActive(true); // Hiển thị hiệu ứng đặc biệt
            playerInfo.ResetPassive(); // Reset Passive về 0

            // 🔥 Phát âm thanh chỉ 1 lần khi bắn
            if (!hasPlayedSpecial)
            {
                hasPlayedSpecial = true; // Đánh dấu đã phát
                AudioManager.Instance.PlayVFX("Special Lazer");
            }

            var specialParticle = specialSkill.GetComponent<ParticleSystem>();
            if (specialParticle != null)
            {
                var emission = specialParticle.emission;
                emission.enabled = true;
            }

            Debug.Log("🔥 Special Skill đã bắn!");

            SetActiveLazer(currentLevel, false); // Tắt laser thường

            Invoke(nameof(DisableSpecialSkill), 5f);
        }
    }

    void AimSpecialSkill()
    {
        Vector3 shootDirection = targetPoint.position - specialSkill.transform.position;
        Quaternion rotationToTarget = Quaternion.LookRotation(shootDirection);
        specialSkill.transform.rotation = rotationToTarget;
    }


    void DisableSpecialSkill()
    {
        specialSkill.SetActive(false);
        if (animator != null)
        {
            animator.SetBool("isSpecial", false);
        }
        // ✅ Bật lại laser sau khi SpecialSkill tắt
        SetActiveLazer(currentLevel, true);
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
            AudioManager.Instance.PlayVFX("Upgrade Lazer"); // Phát âm thanh nâng cấp
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
