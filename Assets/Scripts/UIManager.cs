using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public GameObject creditPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Launch()
    {
        SceneManager.LoadScene("Market");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void open_Credit()
    {
        creditPanel.SetActive(true);
    }

    public void close_Credit()
    {
        creditPanel.SetActive(false);
    }
}
