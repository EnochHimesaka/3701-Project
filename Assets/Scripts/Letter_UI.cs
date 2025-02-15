using UnityEngine;
using UnityEngine.UI; // ȷ������ UI �����ռ�

public class Letter_UI : Interactable  // �̳� Interactable
{
    public GameObject paperUI;  // �� UI ���
    private bool isPlayerNearby = false;

    void Start()
    {
        paperUI.SetActive(false); // ȷ�� UI ��ʼʱ�����ص�
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E)) // �� E �� Paper
        {
            TogglePaperUI();
        }
    }

    private void TogglePaperUI()
    {
        bool isActive = paperUI.activeSelf;
        paperUI.SetActive(!isActive);  // �л� UI ��ʾ/����

        if (!isActive)
        {
            Time.timeScale = 0f; // ��ͣ��Ϸ����ѡ��
        }
        else
        {
            Time.timeScale = 1f; // ������Ϸ
        }
    }

    // ��д Interactable �� OnTriggerEnter �����Ӷ���Ĺ���
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other); // ��׼�Ǳ�ɫ
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other); // ��׼�ǻָ�Ĭ����ɫ
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}