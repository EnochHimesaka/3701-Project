using UnityEngine;
using UnityEngine.UI; // 如果你使用 TextMeshPro 请改成 using TMPro;

public class BookTrigger : MonoBehaviour
{
    public GameObject bookUI;         // UI面板
    public Text bookText;             // 显示文字（如使用TextMeshPro请改类型）
    public GameObject interactHint;   // 👈 提示图片，例如“按E查看”

    private bool isPlayerInside = false;

    void Start()
    {
        if (interactHint != null)
        {
            interactHint.SetActive(false); // 开始时隐藏提示图标
        }

        if (bookUI != null)
        {
            bookUI.SetActive(false); // 默认隐藏书本UI
        }
    }

    void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            bool isOpen = bookUI.activeSelf;
            bookUI.SetActive(!isOpen);

            // 打开书的时候隐藏交互提示
            if (interactHint != null)
                interactHint.SetActive(isOpen == false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;

            if (interactHint != null)
                interactHint.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;

            if (interactHint != null)
                interactHint.SetActive(false);

            if (bookUI != null)
                bookUI.SetActive(false);
        }
    }
}
