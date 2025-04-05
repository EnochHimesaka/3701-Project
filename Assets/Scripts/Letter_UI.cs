using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Letter_UI : Interactable
{
    public GameObject paperUI;
    private bool isPlayerNearby = false;

    public Light[] sceneLights;  // **存多个灯光对象**
    public GameObject powerSwitch; // 断电开关

    void Start()
    {
        paperUI.SetActive(false);
        powerSwitch.SetActive(false); // 初始时锁定电源开关
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetMouseButtonDown(0))
        {
            TogglePaperUI();
        }
    }

    private void TogglePaperUI()
    {
        bool isActive = paperUI.activeSelf;
        paperUI.SetActive(!isActive);

        if (!isActive)
        {
            Time.timeScale = 0f; // 暂停游戏
        }
        else
        {
            Time.timeScale = 1f; // 继续游戏
            StartCoroutine(StartBlackout()); // 关闭 UI 后触发停电倒计时
        }
    }

    private IEnumerator StartBlackout()
    {
        yield return new WaitForSeconds(20f); // **等待10秒**

        // **关闭所有灯光**
        foreach (Light light in sceneLights)
        {
            if (light != null)
            {
                light.enabled = false;
            }
        }

        powerSwitch.SetActive(true); // **解锁开关**
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
