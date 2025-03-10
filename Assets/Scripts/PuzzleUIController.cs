using UnityEngine;

public class PuzzleUIController : MonoBehaviour
{
    public GameObject puzzleUI; // UI 界面
    public PlayerController playerController; // 玩家脚本

    private bool isPuzzleActive = false; // 是否激活解谜界面

    void Start()
    {
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
            // 打开 UI，解锁鼠标，禁用玩家移动
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (playerController != null)
            {
                playerController.enabled = false;
            }
        }
        else
        {
            // 关闭 UI，锁定鼠标，恢复玩家控制
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (playerController != null)
            {
                playerController.enabled = true;
            }
        }
    }
}
