using UnityEngine;
using UnityEngine.SceneManagement;

public class Jumplevel : Interactable
{
    public string nextSceneName; // �� Inspector ������Ҫ��ת�ĳ�������

    private bool isPlayerNearby = false;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty("School"))
        {
            SceneManager.LoadScene("School");
        }
      
    }

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
