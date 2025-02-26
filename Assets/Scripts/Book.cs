using UnityEngine;

public class Book : MonoBehaviour
{
    public GameObject bookUI; // 在 Inspector 里绑定你的 UI 面板

    private bool isBookOpen = false; // 记录书本是否打开

    void Start()
    {
        if (bookUI != null)
        {
            bookUI.SetActive(false); // 确保 UI 初始时是隐藏的
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // 按 T 键打开或关闭书本 UI
        {
            ToggleBookUI();
        }
    }

    private void ToggleBookUI()
    {
        if (bookUI != null)
        {
            isBookOpen = !isBookOpen;
            bookUI.SetActive(isBookOpen); // 切换 UI 显示/隐藏

            if (isBookOpen)
            {
                Time.timeScale = 0f; // 暂停游戏（可选）
            }
            else
            {
                Time.timeScale = 1f; // 继续游戏
            }
        }
    }
}
