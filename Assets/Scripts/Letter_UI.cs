using UnityEngine;
using UnityEngine.UI; // ȷ������ UI �����ռ�

public class Letter_UI : MonoBehaviour
{
    public GameObject paperUI;  // �� UI ���
    public Image crosshair;  // ��׼�� UI
    public Color defaultColor = new Color(1f, 1f, 0.5f, 0.1f); // ǳ�ư�ɫ��10% ͸��
    public Color highlightColor = Color.red; // ����ʱ����ɫ

    private bool isPlayerNearby = false;

    void Start()
    {
        paperUI.SetActive(false); // ȷ�� UI ��ʼʱ�����ص�
        if (crosshair != null)
        {
            SetUIAlpha(crosshair, 0.1f); // ��׼�Ǳ�͸��
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (crosshair != null)
            {
                crosshair.color = highlightColor; // ���뽻����Χʱ��ɫ
                SetUIAlpha(crosshair, 1f); // ����ʱ��ɲ�͸��
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            paperUI.SetActive(false); // �뿪ʱ�Զ��ر� UI
            Time.timeScale = 1f; // ������Ϸ

            if (crosshair != null)
            {
                crosshair.color = defaultColor; // �뿪������Χʱ�ָ���ɫ
                SetUIAlpha(crosshair, 0.01f); // ��׼�ǻָ�͸��
            }
        }
    }

    // ���� UI Image ͸���ȵķ���
    private void SetUIAlpha(Image img, float alpha)
    {
        if (img != null)
        {
            Color newColor = img.color;
            newColor.a = alpha; // ֻ�޸�͸����
            img.color = newColor;
        }
    }
}
