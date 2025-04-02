using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WhiteFadeImage : MonoBehaviour
{
    public Image whiteImage;             // 拖入你的白色 Image
    public float fadeDuration = 1.5f;    // 渐变时间

    private bool hasFaded = false;

    void Start()
    {
        if (whiteImage != null)
        {
            Color c = whiteImage.color;
            c.a = 0f;
            whiteImage.color = c;
            whiteImage.gameObject.SetActive(false); // 开始时隐藏
        }
    }

    public void TriggerFade()
    {
        if (!hasFaded)
        {
            StartCoroutine(FadeToWhite());
            hasFaded = true;
        }
    }

    private IEnumerator FadeToWhite()
    {
        whiteImage.gameObject.SetActive(true);

        float elapsed = 0f;
        Color c = whiteImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsed / fadeDuration);
            whiteImage.color = c;
            yield return null;
        }
    }
}
