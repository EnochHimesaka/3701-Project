using UnityEngine;
using UnityEngine.UI; // 确保引入 UI 命名空间

public class Letter_UI : Interactable  // 继承 Interactable
{
    public GameObject paperUI;  // 绑定 UI 面板
    private bool isPlayerNearby = false;

    void Start()
    {
        paperUI.SetActive(false); // 确保 UI 初始时是隐藏的
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E)) // 按 E 打开 Paper
        {
            TogglePaperUI();
        }
    }

    private void TogglePaperUI()
    {
        bool isActive = paperUI.activeSelf;
        paperUI.SetActive(!isActive);  // 切换 UI 显示/隐藏

        if (!isActive)
        {
            Time.timeScale = 0f; // 暂停游戏（可选）
        }
        else
        {
            Time.timeScale = 1f; // 继续游戏
        }
    }

    // 复写 Interactable 的 OnTriggerEnter 以增加额外的功能
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other); // 让准星变色
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other); // 让准星恢复默认颜色
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}