using UnityEngine;
using UnityEngine.UI; // 确保引入 UI 命名空间

public class Letter_UI : MonoBehaviour
{
    public GameObject paperUI;  // 绑定 UI 面板
    public Image crosshair;  // 绑定准星 UI
    public Color defaultColor = new Color(1f, 1f, 0.5f, 0.1f); // 浅黄白色，10% 透明
    public Color highlightColor = Color.red; // 交互时的颜色

    private bool isPlayerNearby = false;

    void Start()
    {
        paperUI.SetActive(false); // 确保 UI 初始时是隐藏的
        if (crosshair != null)
        {
            SetUIAlpha(crosshair, 0.1f); // 让准星变透明
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (crosshair != null)
            {
                crosshair.color = highlightColor; // 进入交互范围时变色
                SetUIAlpha(crosshair, 1f); // 交互时变成不透明
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            paperUI.SetActive(false); // 离开时自动关闭 UI
            Time.timeScale = 1f; // 继续游戏

            if (crosshair != null)
            {
                crosshair.color = defaultColor; // 离开交互范围时恢复颜色
                SetUIAlpha(crosshair, 0.01f); // 让准星恢复透明
            }
        }
    }

    // 设置 UI Image 透明度的方法
    private void SetUIAlpha(Image img, float alpha)
    {
        if (img != null)
        {
            Color newColor = img.color;
            newColor.a = alpha; // 只修改透明度
            img.color = newColor;
        }
    }
}
