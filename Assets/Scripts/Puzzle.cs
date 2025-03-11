using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PuzzlePiece : MonoBehaviour, IPointerClickHandler
{
    public float rotationAngle = 90f;  // ÿ�ε����ת�Ƕ�
    public float rotationSpeed = 200f; // ��ת�ٶȣ���λ����/�룩

    private RectTransform rectTransform;
    private float targetRotation; // Ŀ��Ƕ�
    private bool isRotating = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        targetRotation = rectTransform.localEulerAngles.z;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("UI С�鱻�����"); // ȷ�����ʱ�����
        if (!isRotating)
        {
            targetRotation = Mathf.Round((targetRotation + rotationAngle) % 360); // ȷ���ǶȲ��ᳬ�� 360
            StartCoroutine(RotateSmoothly());
        }
    }

    private IEnumerator RotateSmoothly()
    {
        isRotating = true;
        float startRotation = rectTransform.localEulerAngles.z;
        float elapsedTime = 0f;

        while (elapsedTime < 0.3f) // ����ת���� 0.3 ��
        {
            elapsedTime += Time.unscaledDeltaTime; // ʹ�� unscaledDeltaTime ���� Time.timeScale Ӱ��
            float newRotation = Mathf.LerpAngle(startRotation, targetRotation, elapsedTime / 0.3f);
            rectTransform.rotation = Quaternion.Euler(0, 0, newRotation);
            yield return null;
        }

        rectTransform.rotation = Quaternion.Euler(0, 0, targetRotation);
        isRotating = false;
    }
}
