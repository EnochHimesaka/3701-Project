using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalLightController : MonoBehaviour
{
    public List<Light> controlledLights = new List<Light>();
    public float globalBlackoutChance = 0.1f;
    public float minBlackoutTime = 0.5f;
    public float maxBlackoutTime = 2f;
    
    void Start()
    {
        // 自动查找场景中所有带AdvancedLightFlicker的灯光
        AdvancedLightFlicker[] flickers = FindObjectsOfType<AdvancedLightFlicker>();
        foreach (AdvancedLightFlicker flicker in flickers)
        {
            controlledLights.Add(flicker.flickerLight);
        }
        
        StartCoroutine(GlobalBlackoutRoutine());
    }
    
    IEnumerator GlobalBlackoutRoutine()
    {
        while (true)
        {
            if (Random.value < globalBlackoutChance)
            {
                // 保存原始强度
                float[] originalIntensities = new float[controlledLights.Count];
                for (int i = 0; i < controlledLights.Count; i++)
                {
                    if (controlledLights[i] != null)
                    {
                        originalIntensities[i] = controlledLights[i].intensity;
                        controlledLights[i].intensity = 0;
                    }
                }
                
                float blackoutTime = Random.Range(minBlackoutTime, maxBlackoutTime);
                yield return new WaitForSeconds(blackoutTime);
                
                // 逐个恢复灯光，带有随机延迟
                for (int i = 0; i < controlledLights.Count; i++)
                {
                    if (controlledLights[i] != null)
                    {
                        StartCoroutine(RestoreLight(controlledLights[i], originalIntensities[i], Random.Range(0f, 1f)));
                    }
                    yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
                }
            }
            
            yield return new WaitForSeconds(Random.Range(10f, 30f));
        }
    }
    
    IEnumerator RestoreLight(Light light, float targetIntensity, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        float duration = Random.Range(0.5f, 2f);
        float elapsed = 0;
        
        while (elapsed < duration)
        {
            if (light != null)
            {
                light.intensity = Mathf.Lerp(0, targetIntensity, elapsed/duration) * Random.Range(0.8f, 1.2f);
            }
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        if (light != null)
        {
            light.intensity = targetIntensity;
        }
    }
}