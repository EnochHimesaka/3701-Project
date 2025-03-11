using UnityEngine;
using UnityEngine.SceneManagement;

public class Jumplevel : Interactable
{
    public string nextSceneName; // 在 Inspector 里设置要跳转的场景名称

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
        base.OnTriggerEnter(other); // 让准星变色
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other); // 让准星恢复默认颜色
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
