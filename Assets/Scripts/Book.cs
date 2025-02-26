using UnityEngine;

public class Book : MonoBehaviour
{
    public GameObject bookUI; // �� Inspector ������ UI ���

    private bool isBookOpen = false; // ��¼�鱾�Ƿ��

    void Start()
    {
        if (bookUI != null)
        {
            bookUI.SetActive(false); // ȷ�� UI ��ʼʱ�����ص�
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // �� T ���򿪻�ر��鱾 UI
        {
            ToggleBookUI();
        }
    }

    private void ToggleBookUI()
    {
        if (bookUI != null)
        {
            isBookOpen = !isBookOpen;
            bookUI.SetActive(isBookOpen); // �л� UI ��ʾ/����

            if (isBookOpen)
            {
                Time.timeScale = 0f; // ��ͣ��Ϸ����ѡ��
            }
            else
            {
                Time.timeScale = 1f; // ������Ϸ
            }
        }
    }
}
