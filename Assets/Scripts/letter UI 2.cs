using UnityEngine;
using UnityEngine.UI; // �����ʹ�� TextMeshPro ��ĳ� using TMPro;

public class BookTrigger : MonoBehaviour
{
    public GameObject bookUI;       // UI���
    public Text bookText;           // Ҫ��ʾ�����֣������TextMeshProUGUI��������ͣ�


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
