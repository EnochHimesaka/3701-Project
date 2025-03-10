using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleUIController : MonoBehaviour, IPointerClickHandler
{
    public GameObject puzzleUI; // UI 界面
    public PlayerController playerController; // 玩家脚本
    public float rotationAngle = 90f; // 每次点击旋转角度
    public float rotationSpeed = 200f; // 旋转速度
    private bool isRotating = false;
    private float targetRotation;
    private bool isPuzzleActive = false; // 是否激活解谜界面

    void Start()
    {
        targetRotation = transform.eulerAngles.z;
        puzzleUI.SetActive(false); // 初始时 UI 关闭
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TogglePuzzleUI();
        }
    }

    public void TogglePuzzleUI()
    {
        isPuzzleActive = !isPuzzleActive;
        puzzleUI.SetActive(isPuzzleActive);

        if (isPuzzleActive)
        {
            // 解锁鼠标
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // 禁用玩家控制
            if (playerController != null)
            {
                playerController.enabled = false; // 关闭玩家控制
            }
        }
        else
        {
            // 锁定鼠标
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // 启用玩家控制
            if (playerController != null)
            {
                playerController.enabled = true; // 重新启用玩家控制
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isRotating)
        {
            targetRotation += rotationAngle; // 让 UI 旋转 90°
            StartCoroutine(RotateSmoothly());
        }
    }

    private System.Collections.IEnumerator RotateSmoothly()
    {
        isRotating = true;
        float currentRotation = transform.eulerAngles.z;
        float time = 0f;

        while (time < 1f)
        {
            time += Time.deltaTime * (rotationSpeed / rotationAngle);
            float newRotation = Mathf.Lerp(currentRotation, targetRotation, time);
            transform.eulerAngles = new Vector3(0, 0, newRotation);
            yield return null;
        }

        transform.eulerAngles = new Vector3(0, 0, targetRotation); // 确保旋转最终停在正确的角度
        isRotating = false;
    }
}
