using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PuzzlePiece : MonoBehaviour, IPointerClickHandler
{
    public float rotationAngle = 90f;  // 每次点击旋转角度
    public float rotationSpeed = 200f; // 旋转速度（单位：度/秒）
    public float correctRotation = 0f; // 目标正确角度

    private RectTransform rectTransform;
    private float targetRotation;
    private bool isRotating = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        targetRotation = rectTransform.localEulerAngles.z;

        
        ShuffleRotation();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
     
        if (!isRotating)
        {
            targetRotation = Mathf.Round((targetRotation + rotationAngle) % 360);
            StartCoroutine(RotateSmoothly());
        }
    }

    private IEnumerator RotateSmoothly()
    {
        isRotating = true;
        float startRotation = rectTransform.localEulerAngles.z;
        float elapsedTime = 0f;

        while (elapsedTime < 0.3f)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float newRotation = Mathf.LerpAngle(startRotation, targetRotation, elapsedTime / 0.3f);
            rectTransform.rotation = Quaternion.Euler(0, 0, newRotation);
            yield return null;
        }

        rectTransform.rotation = Quaternion.Euler(0, 0, targetRotation);
        isRotating = false;
        FindObjectOfType<PuzzleUIController>().CheckPuzzleCompletion();
    }

    
    public bool IsCorrectlyRotated()
    {
        float currentRotation = rectTransform.localEulerAngles.z;
        return Mathf.Abs(currentRotation - correctRotation) < 5f; // 允许 5° 误差
    }

    private void ShuffleRotation()
    {
        int randomRotations = Random.Range(0, 4); // 0, 1, 2, 3 -> 0, 90, 180, 270 度
        targetRotation = (correctRotation + randomRotations * 90) % 360;
        rectTransform.rotation = Quaternion.Euler(0, 0, targetRotation);
    }


}
