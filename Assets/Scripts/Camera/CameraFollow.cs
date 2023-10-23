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
}
