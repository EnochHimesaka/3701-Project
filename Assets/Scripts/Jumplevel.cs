using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Jumplevel : Interactable
{
    public string nextSceneName = "Lab";
    private bool isPlayerNearby = false;

    [Header("ºÚÆÁµ­³ö")]
    public CanvasGroup blackScreen;
    public float fadeDuration = 1.5f;

    void Update()
    {
        if (isPlayerNearby && PuzzleUIController.puzzleCleared && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(TransitionToNextScene());
        }
    }

    private IEnumerator TransitionToNextScene()
    {
        if (blackScreen != null)
        {
            blackScreen.gameObject.SetActive(true);
            float elapsed = 0f;

            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                blackScreen.alpha = Mathf.Clamp01(elapsed / fadeDuration);
                yield return null;
            }
        }

        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("Lab");
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
