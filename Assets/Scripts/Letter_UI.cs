using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Letter_UI : Interactable
{
    public GameObject paperUI;
    private bool isPlayerNearby = false;

    public Light[] sceneLights;       // 场景灯光
    public GameObject powerSwitch;    // 电源开关

    public GameObject objectToDestroy;           // 第一次交互时销毁的物体
    public GameObject blackoutDestroyObject;     // 25秒后关灯时销毁的物体 ✅

    private bool hasInteracted = false; // 防止重复销毁

    void Start()
    {
        paperUI.SetActive(false);
        powerSwitch.SetActive(false); // 初始时关闭电源开关
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
            Time.timeScale = 1f; // 恢复游戏
            StartCoroutine(StartBlackout());

            if (!hasInteracted && objectToDestroy != null)
            {
                Destroy(objectToDestroy);       // 交互后立即销毁
                hasInteracted = true;
            }
        }
    }

    private IEnumerator StartBlackout()
    {
        yield return new WaitForSeconds(25f); // 等待 25 秒

        foreach (Light light in sceneLights)
        {
            if (light != null)
            {
                light.enabled = false;
            }
        }

        powerSwitch.SetActive(true); // 打开电源开关

        if (blackoutDestroyObject != null)
        {
            Destroy(blackoutDestroyObject); // ✅ 25秒后销毁目标物体
        }
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
