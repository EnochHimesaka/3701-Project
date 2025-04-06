using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class USB : MonoBehaviour
{
    public GameObject interactHintUI;  // "按 E 交互" 提示
    public GameObject usbIconUI;       // ✅ 获得 USB 的 UI 图标

    public static int usbCount = 0;

    private bool isPlayerNearby = false;

    void Start()
    {
        if (interactHintUI != null)
            interactHintUI.SetActive(false);

        if (usbIconUI != null)
            usbIconUI.SetActive(false); // 初始隐藏获得提示
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            CollectUSB();
        }
    }

    private void CollectUSB()
    {
        usbCount++;
        Debug.Log("USB collected! Total: " + usbCount);

        if (interactHintUI != null)
            interactHintUI.SetActive(false);

        if (usbIconUI != null)
            StartCoroutine(ShowUSBIcon()); // ✅ 播放“获得提示”

        Destroy(gameObject);
    }

    private IEnumerator ShowUSBIcon()
    {
        usbIconUI.SetActive(true);
        yield return new WaitForSeconds(2f); // 显示 2 秒
        usbIconUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;

            if (interactHintUI != null)
                interactHintUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;

            if (interactHintUI != null)
                interactHintUI.SetActive(false);
        }
    }
}
