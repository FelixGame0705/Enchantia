using System.Collections;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    public Vector2 minBounds;
    public Vector2 maxBounds;
    public float shakeMagnitude = 0.1f; // Magnitude of camera shake
    public float shakeDuration = 0.2f; // Duration of camera shake

    private Vector3 originalPosition;
    private Vector3 desiredPosition;

    private bool isShaking = false;
    private float shakeDurationTimer = 0f;
    private Vector3 shakeOffset = Vector3.zero;

    private void Start()
    {
        originalPosition = transform.position;
        StartFollow();
    }

    private void LateUpdate()
    {
        if (target == null) return;
        desiredPosition = target.position + offset + shakeOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(
            Mathf.Clamp(smoothedPosition.x, minBounds.x, maxBounds.x),
            Mathf.Clamp(smoothedPosition.y, minBounds.y, maxBounds.y),
            originalPosition.z // Keep the original Z position
        );

        if (isShaking)
        {
            ShakeCamera();
        }
    }

    public void StartCameraShake()
    {
        isShaking = true;
        shakeDurationTimer = shakeDuration;
    }

    private void ShakeCamera()
    {
        if (shakeDurationTimer > 0f)
        {
            shakeOffset = Random.insideUnitSphere * shakeMagnitude;
            shakeDurationTimer -= Time.deltaTime;
        }
        else
        {
            isShaking = false;
            shakeOffset = Vector3.zero;
        }
    }

    private void StartFollow()
    {
        // Set the initial position of the camera to the middle of the screen
        Vector3 initialPosition = new Vector3(
            (minBounds.x + maxBounds.x) / 2f,
            (minBounds.y + maxBounds.y) / 2f,
            originalPosition.z
        );

        transform.position = initialPosition;

        // Start the coroutine to smoothly move and zoom the camera towards the target
        StartCoroutine(FollowTarget());
    }

    private IEnumerator FollowTarget()
    {
        yield return new WaitUntil(()=>target != null);
        float elapsedTime = 0f;
        float followDuration = 1f; // Adjust the duration as needed

        while (elapsedTime < followDuration)
        {
            // Calculate the new position and size based on the lerp progress
            Vector3 newPosition = Vector3.Lerp(transform.position, target.position, elapsedTime / followDuration);
            float newSize = Mathf.Lerp(12f, 8f, elapsedTime / followDuration); // Adjust the sizes as needed
            Debug.Log("New size" + newSize);
            // Set the camera position and orthographic size
            transform.position = new Vector3(
                Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x),
                Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y),
                originalPosition.z
            );
            Camera.main.orthographicSize = newSize;

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Set the final position and size to ensure accuracy
        transform.position = new Vector3(
            Mathf.Clamp(target.position.x, minBounds.x, maxBounds.x),
            Mathf.Clamp(target.position.y, minBounds.y, maxBounds.y),
            originalPosition.z
        );
        Camera.main.orthographicSize = 8f; // Set the final size

        // Start camera follow normally
        //StartCoroutine(FollowTargetRoutine());
    }
}
