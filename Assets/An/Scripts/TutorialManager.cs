using UnityEngine;
using UnityEngine.Playables;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerWeapon playerWeapon;

    [SerializeField] private GameObject tutorpart6;

    [SerializeField] private ParticleSystem pfxTarget;

    [SerializeField] private float distancePass = 20;

    [SerializeField] private TextMeshProUGUI guideMoveTarget;

    [SerializeField] private GameObject ufo;

    [SerializeField] private Button NextTutorial;

    private bool isCheckMoveTarget;
    private bool canPressSpace = false; // Biến kiểm tra có thể bấm Space không

    float distanceTarget;

    void Start()
    {
        playerMovement.IsPlayingTutorial = true;
        playerWeapon.IsPlayingTutorial = true;

        playableDirector.stopped += TimeLineCompleted;

        playableDirector.Play();
    }

    private void OnDestroy()
    {
        playableDirector.stopped -= TimeLineCompleted;
    }

    private void Update()
    {
        if (!isCheckMoveTarget)
            return;

        if (playerMovement.transform.position.z > pfxTarget.transform.position.z - distancePass)
        {
            isCheckMoveTarget = false;
            StartCoroutine(IEGuideUserShoot());
        }

        // Nhấn Space để next tutorial
        if (canPressSpace && Input.GetKeyDown(KeyCode.Space))
        {
            NextTutorial.onClick.Invoke(); // Giả lập click vào nút NextTutorial
        }
    }

    private void TimeLineCompleted(PlayableDirector playableDirector)
    {
        StartCoroutine(IETutorialPartSix());
    }

    private IEnumerator IETutorialPartSix()
    {
        NextTutorial.gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        canPressSpace = true; // Cho phép bấm Space

        yield return new WaitForSeconds(2);

        tutorpart6.SetActive(false);

        playerMovement.IsPlayingTutorial = false;
        playerWeapon.IsPlayingTutorial = false;

        StartCoroutine(IEGuideUserMove());
    }

    private IEnumerator IEGuideUserMove()
    {
        yield return new WaitForSeconds(1);

        playerMovement.transform.position = new Vector3(0, 80, 167);

        guideMoveTarget.gameObject.SetActive(true);

        guideMoveTarget.text = "Tutorial is Finished";

        yield return new WaitForSeconds(2);

        guideMoveTarget.text = "Your Turn";

        yield return new WaitForSeconds(1);

        guideMoveTarget.text = "Move Your Target";

        pfxTarget.Play();

        isCheckMoveTarget = true;
    }

    private IEnumerator IEGuideUserShoot()
    {
        guideMoveTarget.text = "Done";

        yield return new WaitForSeconds(1);

        pfxTarget.Stop();

        guideMoveTarget.text = "Shoot Enemy";

        ufo.SetActive(true);
    }

    private void DoneTutorial()
    {
        StartCoroutine(IELoadScene());
    }

    private IEnumerator IELoadScene()
    {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("Fight");
        Cursor.visible = false; // Ẩn con trỏ chuột
    }

    public void FinishTutorial()
    {
        SceneManager.LoadScene("Fight");
    }
}
