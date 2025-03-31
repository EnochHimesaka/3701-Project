using UnityEngine;
using UnityEngine.UI; // 如果你使用 TextMeshPro 请改成 using TMPro;

public class BookTrigger : MonoBehaviour
{
    public GameObject bookUI;       // UI面板
    public Text bookText;           // 要显示的文字（如果是TextMeshProUGUI，请改类型）


    private bool isPlayerInside = false;

    void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            if (!bookUI.activeSelf)
            {
                bookUI.SetActive(true);
                //bookText.text = content;
            }
            else
            {
                bookUI.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            bookUI.SetActive(false);
        }
    }
}
