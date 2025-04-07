using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpToLevel : MonoBehaviour
{
    public string sceneName = "end";

    public void JumpToScene()
    {
        if (USB.usbCount > 0)
        {
            Debug.Log("USB �ѻ�ȡ����ת��������" + sceneName);
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log("USB δ��ȡ���޷���ת��");
        }
    }
}
