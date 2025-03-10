using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IPointerClickHandler
{
    public float rotationAngle = 90f;  // 每次点击旋转角度
    public float rotationSpeed = 5f; // 旋转速度

    private RectTransform rectTransform;
    private float currentRotation; // 当前角度
    private float targetRotation; // 目标角度
    private bool isRotating = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        currentRotation = rectTransform.localEulerAngles.z;
        targetRotation = currentRotation;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("UI 小块被点击！"); // 确保点击时有输出
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
            time += Time.unscaledDeltaTime * rotationSpeed; // 使用 unscaledDeltaTime，避免受 Time.timeScale 影响
            float newRotation = Mathf.Lerp(startRotation, targetRotation, time);
            rectTransform.localEulerAngles = new Vector3(0, 0, newRotation);
            yield return null;
        }

        rectTransform.localEulerAngles = new Vector3(0, 0, targetRotation);
        currentRotation = targetRotation;
        isRotating = false;
    }
}
