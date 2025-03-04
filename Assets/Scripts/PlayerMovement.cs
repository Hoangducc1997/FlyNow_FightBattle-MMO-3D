using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float controlSpeed = 1.0f;
    [SerializeField] float xClampRange = 1.0f;
    [SerializeField] float yClampRange = 1.0f;
    [SerializeField] float controlRollFactor = 1.0f; // Rotate z
    [SerializeField] float controlPitchFactor = 1.0f; // Rotate y
    [SerializeField] float controlYawFactor = 1.0f; // Rotate x
    [SerializeField] float rotationSpeed = 1.0f;
    [SerializeField] float increaseSpeed = 30;

    [SerializeField] float speedMove = 20;

    Vector2 movement;


    public float z;

    public bool IsPlayingTutorial { get; set;}
  
    void Update()
    {
        if (IsPlayingTutorial)
            return;

        transform.Translate(Vector3.forward * speedMove * Time.deltaTime);

        ProcessTranslation();
        ProcessRotation();
    }
    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    void ProcessTranslation()
    {
        float xOffset = movement.x * controlSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xClampRange, xClampRange);

        float yOffset = movement.y * controlSpeed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        float ClampedYPos = Mathf.Clamp(rawYPos, -yClampRange, yClampRange);

        transform.localPosition = new Vector3(clampedXPos, ClampedYPos, transform.localPosition.z);
    }

    void ProcessRotation()
    {
        float roll = -controlRollFactor * movement.x;
        float pitch = -controlPitchFactor * movement.y;
        float yaw = -controlYawFactor * movement.x;
        Quaternion targetRotation = Quaternion.Euler(pitch ,yaw , roll);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
