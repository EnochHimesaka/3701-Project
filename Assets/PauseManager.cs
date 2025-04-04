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
        // ���� ESC ���л���ͣ״̬
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
        Time.timeScale = 0f; // ��ͣʱ������
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void ResumeGame()
    {
        corsshair.SetActive(false);
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // �ָ�ʱ������
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // �༭�����˳�����ģʽ
#else
        Application.Quit(); // ������˳�Ӧ��
#endif
    }
}
