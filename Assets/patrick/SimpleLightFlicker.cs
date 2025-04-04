using UnityEngine;
using System.Collections;

public class SimpleLightFlicker : MonoBehaviour
{
    public Light flickerLight;
    public float minIntensity = 0f;
    public float maxIntensity = 1f;
    public float minInterval = 0.05f;
    public float maxInterval = 0.2f;

    void Start()
    {
        if (flickerLight == null)
            flickerLight = GetComponent<Light>();
        
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            flickerLight.intensity = Random.Range(minIntensity, maxIntensity);
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
        }
    }
}