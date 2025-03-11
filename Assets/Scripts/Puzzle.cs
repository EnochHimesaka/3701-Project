using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PuzzlePiece : MonoBehaviour, IPointerClickHandler
{
    public float rotationAngle = 90f;  // 每次点击旋转角度
    public float rotationSpeed = 200f; // 旋转速度（单位：度/秒）

    private RectTransform rectTransform;
    private float targetRotation; // 目标角度
    private bool isRotating = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        targetRotation = rectTransform.localEulerAngles.z;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("UI 小块被点击！"); // 确保点击时有输出
        if (!isRotating)
        {
            targetRotation = Mathf.Round((targetRotation + rotationAngle) % 360); // 确保角度不会超出 360
            StartCoroutine(RotateSmoothly());
        }
    }

    private IEnumerator RotateSmoothly()
    {
        isRotating = true;
        float startRotation = rectTransform.localEulerAngles.z;
        float elapsedTime = 0f;

        while (elapsedTime < 0.3f) // 让旋转持续 0.3 秒
        {
            elapsedTime += Time.unscaledDeltaTime; // 使用 unscaledDeltaTime 避免 Time.timeScale 影响
            float newRotation = Mathf.LerpAngle(startRotation, targetRotation, elapsedTime / 0.3f);
            rectTransform.rotation = Quaternion.Euler(0, 0, newRotation);
            yield return null;
        }

        rectTransform.rotation = Quaternion.Euler(0, 0, targetRotation);
        isRotating = false;
    }
}
