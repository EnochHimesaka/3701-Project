using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Letter_UI : Interactable
{
    public GameObject paperUI;
    private bool isPlayerNearby = false;

    public Light[] sceneLights;  // **�����ƹ����**
    public GameObject powerSwitch; // �ϵ翪��

    void Start()
    {
        paperUI.SetActive(false);
        powerSwitch.SetActive(false); // ��ʼʱ������Դ����
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
            Time.timeScale = 0f; // ��ͣ��Ϸ
        }
        else
        {
            Time.timeScale = 1f; // ������Ϸ
            StartCoroutine(StartBlackout()); // �ر� UI �󴥷�ͣ�絹��ʱ
        }
    }

    private IEnumerator StartBlackout()
    {
        yield return new WaitForSeconds(27f); // **�ȴ�10��**

        // **�ر����еƹ�**
        foreach (Light light in sceneLights)
        {
            if (light != null)
            {
                light.enabled = false;
            }
        }

        powerSwitch.SetActive(true); // **��������**
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
