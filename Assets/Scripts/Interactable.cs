using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public Image crosshair;  // ��׼�� UI
    public GameObject interactPrompt; // �󶨽�����ʾ UI�����硰��E��������

    private void Start()
    {
        if (crosshair == null)
        {
            GameObject crosshairObject = GameObject.Find("Point");
            if (crosshairObject != null)
            {
                crosshair = crosshairObject.GetComponent<Image>();
            }
        }

        if (crosshair != null)
        {
            Color tempColor = crosshair.color;
            tempColor.a = 0.3f;  // **ȷ��׼��Ĭ���ǰ�͸���ɼ�**
            crosshair.color = tempColor;
        }

        if (interactPrompt != null)
        {
            interactPrompt.SetActive(false); // **��ʼ���ؽ�����ʾ**
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (interactPrompt != null)
            {
                interactPrompt.SetActive(true); // **��ʾ������ʾ**
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (interactPrompt != null)
            {
                interactPrompt.SetActive(false); // **���ؽ�����ʾ**
            }
        }
    }
}
