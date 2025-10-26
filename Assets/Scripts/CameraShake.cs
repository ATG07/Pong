using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    [Header("Shake Settings")]
    public Transform camTransform;
    private float shakeDuration = 0.5f;
    private float shakeMagnitude = 0.7f;

    private Vector3 originalPos;

    private void Awake()
    {
        if (camTransform == null)
            camTransform = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    public void TriggerShake(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        float currentShakeDuration = duration;

        while (currentShakeDuration > 0)
        {
            Vector3 randomOffset = Random.insideUnitSphere * magnitude;
            randomOffset.z = 0;

            camTransform.localPosition = originalPos + randomOffset;

            currentShakeDuration -= Time.deltaTime;
            yield return null;
        }

        camTransform.localPosition = originalPos;
    }
}
