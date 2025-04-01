using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InteractableConsoleUI : MonoBehaviour
{
    public GameObject uiPanel;             // ����������ť��UI���
    public GameObject anotherUIPanel;      // �ڶ���UI���棨������ƽ��棩

    public Button openButton;              // ����һ��UI�İ�ť
    public Button quitButton;              // �˳���ť

    private bool isPlayerInRange = false;

    void Start()
    {
        if (uiPanel != null) uiPanel.SetActive(false);

        // ����ť��ӵ���¼�
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
            uiPanel.SetActive(false); // ��ѡ���رյ�ǰUI
        }
    }

    void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game"); // �ڱ༭���в����˳�����������
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f; // ��ѡ����ͣ��Ϸ
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
