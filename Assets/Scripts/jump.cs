using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpToLevel : MonoBehaviour
{
    public string sceneName = "end";

    public void JumpToScene()
    {
        if (USB.usbCount > 0)
        {
            Debug.Log("USB 已获取，跳转至场景：" + sceneName);
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log("USB 未获取，无法跳转！");
        }
    }
}
