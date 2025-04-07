using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractableConsoleUI : MonoBehaviour
{
    public GameObject uiPanel;             // UI面板
    public Button openButton;              // 普通按钮（将会变成退出）
    public Button secretButton;            // 真结局按钮

    private bool isPlayerInRange = false;
    private bool hasEnabledSecretButton = false;

    public Image whiteScreenImage;         // 全屏白图
    public AudioSource bgm;                // 背景音乐

    void Start()
    {
        if (uiPanel != null) uiPanel.SetActive(false);

        // 设置白图初始透明
        if (whiteScreenImage != null)
        {
            whiteScreenImage.canvasRenderer.SetAlpha(0f);
        }

        // 初始隐藏真结局按钮
        if (secretButton != null)
        {
            secretButton.gameObject.SetActive(false);
        }
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

        // USB 条件达成时显示隐藏结局按钮
        if (!hasEnabledSecretButton && USB.usbCount > 0)
        {
            Debug.Log("USB 已获取，显示隐藏结局按钮！");
            if (secretButton != null)
            {
                secretButton.gameObject.SetActive(true);
                hasEnabledSecretButton = true;
            }
        }
    }

    public void TriggerSecretEnding()
    {
        StartCoroutine(SecretEndingSequence());
    }

    IEnumerator SecretEndingSequence()
    {
        // 白屏淡入
        if (whiteScreenImage != null)
        {
            whiteScreenImage.CrossFadeAlpha(1f, 2f, true);
        }

        if (bgm != null)
        {
            bgm.Stop();
        }

        yield return new WaitForSeconds(2f);

        // 将 openButton 替换为“退出游戏”
        if (openButton != null)
        {
            //openButton.GetComponentInChildren<Text>().text = "退出游戏";
            openButton.onClick.RemoveAllListeners();
            openButton.onClick.AddListener(QuitGame);
        }

        // 隐藏 secretButton
        if (secretButton != null)
        {
            secretButton.gameObject.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
            if (uiPanel != null)
                uiPanel.SetActive(false);

            LockCursor();
        }
    }
}
