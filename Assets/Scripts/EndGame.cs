using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class InteractableConsoleUI : MonoBehaviour
{
    public GameObject uiPanel;             // 包含两个按钮的UI面板
   

    public Button openButton;              // 打开另一个UI的按钮
    

    private bool isPlayerInRange = false;
    public Image yuanshen_image;
    public AudioSource yuanshen_audio;
    public AudioSource qidong_audio;
    public AudioSource bgm;
    public GameObject creditPanel;
    public GameObject imagePanel;
    public GameObject leiwenliang;

    void Start()
    {
        if (uiPanel != null) uiPanel.SetActive(false);

      
      
       

        yuanshen_image.canvasRenderer.SetAlpha(0.0f);
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (uiPanel != null)
            {
                uiPanel.SetActive(true);
                UnlockCursor();
            }
        }
    }

    public void Openyuanshen()
    {
          yuanshen_image.CrossFadeAlpha(1, 1, true);
         bgm.Stop();
        yuanshen_audio.Play();
        qidong_audio.Play();

        imagePanel.SetActive(false);
        leiwenliang.SetActive(false);
        StartCoroutine(backtoMain());


    }
    IEnumerator backtoMain()
    {
        yield return new WaitForSeconds(10.0f);
        BacktoMain();
    }

    void BacktoMain()
    {
        SceneManager.LoadScene("Start");
        creditPanel.SetActive(true);
    }


    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game"); 
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //Time.timeScale = 0f; // 可选：暂停游戏
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //Time.timeScale = 1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (uiPanel != null) uiPanel.SetActive(false);
            LockCursor();
        }
    }
}
