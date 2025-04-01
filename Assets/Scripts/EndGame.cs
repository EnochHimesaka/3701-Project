using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InteractableConsoleUI : MonoBehaviour
{
    public GameObject uiPanel;             // 包含两个按钮的UI面板
    public GameObject anotherUIPanel;      // 第二个UI界面（例如控制界面）

    public Button openButton;              // 打开另一个UI的按钮
    public Button quitButton;              // 退出按钮

    private bool isPlayerInRange = false;

    void Start()
    {
        if (uiPanel != null) uiPanel.SetActive(false);

        // 给按钮添加点击事件
        openButton.onClick.AddListener(OpenAnotherUI);
        quitButton.onClick.AddListener(QuitGame);
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (uiPanel != null)
            {
                uiPanel.SetActive(true);
                UnlockCursor();
            }
        }
    }

    void OpenAnotherUI()
    {
        if (anotherUIPanel != null)
        {
            anotherUIPanel.SetActive(true);
            uiPanel.SetActive(false); // 可选：关闭当前UI
        }
    }

    void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game"); // 在编辑器中不会退出，用来测试
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f; // 可选：暂停游戏
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (uiPanel != null) uiPanel.SetActive(false);
            LockCursor();
        }
    }
}
