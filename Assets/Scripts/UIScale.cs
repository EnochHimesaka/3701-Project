using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public AudioSource audio;
    public void OnPointerEnter(PointerEventData eventData)
    {
        audio.Play();
        transform.localScale = new Vector2(1.1f, 1.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector2(1f, 1f);
    }
}
