using UnityEngine;
using UnityEngine.EventSystems; // 引入事件系统

public class PuzzleUIController : MonoBehaviour
{
    public GameObject puzzleUI; // UI 界面
    public PlayerController playerController; // 玩家脚本

    private bool isPuzzleActive = false; // 是否激活解谜界面
    private int clickCount = 0; // 鼠标点击计数
    private int requiredClicks = 10; // 需要的点击次数

    void Start()
    {
        puzzleUI.SetActive(false); // 初始时 UI 关闭
    }

    void Update()
    {
        if (isPuzzleActive && Input.GetMouseButtonDown(0))
        {
            // 确保点击不会被 UI 拦截
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                clickCount++;
                Debug.Log("点击次数: " + clickCount);

                if (clickCount >= requiredClicks)
                {
                    WinPuzzle();
                }
            }
            else
            {
                Debug.Log("点击无效，被 UI 挡住了！");
            }
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

    public void WinPuzzle()
    {
        Debug.Log("解谜成功！");
        isPuzzleActive = false;
        puzzleUI.SetActive(false); // 关闭 UI

        // 还原鼠标控制
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 还原玩家控制
        if (playerController != null)
        {
            playerController.enabled = true;
        }
    }
}
