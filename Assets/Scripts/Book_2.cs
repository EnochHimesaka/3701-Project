using UnityEngine;

public class Book_2 : MonoBehaviour
{
    public GameObject bookUI;         // UI ��壬��ʾ�������
    public GameObject interactHint;   // ��ʾͼƬ�����硰��E�鿴��

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
