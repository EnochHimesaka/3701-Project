using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public Image crosshair;  // 绑定准星 UI
    public GameObject interactPrompt; // 绑定交互提示 UI（比如“按E交互”）

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
            tempColor.a = 0.3f;  // **确保准星默认是半透明可见**
            crosshair.color = tempColor;
        }

        if (interactPrompt != null)
        {
            interactPrompt.SetActive(false); // **初始隐藏交互提示**
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (interactPrompt != null)
            {
                interactPrompt.SetActive(true); // **显示交互提示**
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (interactPrompt != null)
            {
                interactPrompt.SetActive(false); // **隐藏交互提示**
            }
        }
    }
}
