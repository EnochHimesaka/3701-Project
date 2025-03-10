using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IPointerClickHandler
{
    public float rotationAngle = 90f;  // ÿ�ε����ת�Ƕ�
    public float rotationSpeed = 5f; // ��ת�ٶ�

    private RectTransform rectTransform;
    private float currentRotation; // ��ǰ�Ƕ�
    private float targetRotation; // Ŀ��Ƕ�
    private bool isRotating = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        currentRotation = rectTransform.localEulerAngles.z;
        targetRotation = currentRotation;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("UI С�鱻�����"); // ȷ�����ʱ�����
        if (!isRotating)
        {
            targetRotation += rotationAngle;
            StartCoroutine(RotateSmoothly());
        }
    }

    private System.Collections.IEnumerator RotateSmoothly()
    {
        isRotating = true;
        float time = 0f;
        float startRotation = rectTransform.localEulerAngles.z;

        while (time < 1f)
        {
            time += Time.unscaledDeltaTime * rotationSpeed; // ʹ�� unscaledDeltaTime�������� Time.timeScale Ӱ��
            float newRotation = Mathf.Lerp(startRotation, targetRotation, time);
            rectTransform.localEulerAngles = new Vector3(0, 0, newRotation);
            yield return null;
        }

        rectTransform.localEulerAngles = new Vector3(0, 0, targetRotation);
        currentRotation = targetRotation;
        isRotating = false;
    }
}
