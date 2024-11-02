using System.Collections;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Duración de la rotación
    public float rotationDuration = 1.0f;

    private bool isRotating = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!isRotating)
        {
            StartCoroutine(RotateObject());
        }
    }

    private IEnumerator RotateObject()
    {
        isRotating = true;

        Quaternion targetRotation = transform.rotation * Quaternion.Euler(180, 0, 0);
        Quaternion startRotation = transform.rotation;
        float elapsedTime = 0;

        while (elapsedTime < rotationDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation; // Asegúrate de que termine en la rotación deseada
        isRotating = false;
    }
}