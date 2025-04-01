using UnityEngine;

public class Book_2 : MonoBehaviour
{
    public GameObject bookUI;         // UI 面板，显示书的内容
    public GameObject interactHint;   // 提示图片，例如“按E查看”

    private bool isPlayerInRange = false;
    private bool isBookOpen = false;

    void Start()
    {
        if (interactHint != null)
            interactHint.SetActive(false);

        if (bookUI != null)
            bookUI.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            isBookOpen = !isBookOpen;
            bookUI.SetActive(isBookOpen);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (interactHint != null)
                interactHint.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (interactHint != null)
                interactHint.SetActive(false);

            if (isBookOpen)
            {
                isBookOpen = false;
                if (bookUI != null)
                    bookUI.SetActive(false);
            }
        }
    }
}
