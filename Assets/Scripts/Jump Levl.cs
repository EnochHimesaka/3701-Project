using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class JumpLevel : MonoBehaviour
{
    public string nextSceneName = "NextScene";         // 目标场景名称
    public CanvasGroup blackScreen;                    // 可选黑幕过渡
    public float fadeDuration = 1f;                    // 淡出时间
    public GameObject interactHint;                    // 提示图片（如“Press E to enter”）

    private bool isPlayerNearby = false;

    void Start()
    {
        if (interactHint != null)
            interactHint.SetActive(false);

        if (blackScreen != null)
        {
            blackScreen.alpha = 0;
            blackScreen.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(LoadSceneWithFade());
        }
    }

    private IEnumerator LoadSceneWithFade()
    {
        if (blackScreen != null)
        {
            blackScreen.gameObject.SetActive(true);
            float t = 0f;
            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                blackScreen.alpha = Mathf.Clamp01(t / fadeDuration);
                yield return null;
            }
        }

        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(nextSceneName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (interactHint != null)
                interactHint.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (interactHint != null)
                interactHint.SetActive(false);
        }
    }
}
