using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public Button resumeButton;
    public Button exitButton;
    public GameObject corsshair;

    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false);
        resumeButton.onClick.AddListener(ResumeGame);
        exitButton.onClick.AddListener(ExitGame);
    }

    void Update()
    {
        // 按下 ESC 键切换暂停状态
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                
                ResumeGame();
            }
                
                

            else
                PauseGame();
        }
    }

    void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // 暂停时间流动
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void ResumeGame()
    {
        corsshair.SetActive(false);
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // 恢复时间流动
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 编辑器中退出播放模式
#else
        Application.Quit(); // 打包后退出应用
#endif
    }
}
