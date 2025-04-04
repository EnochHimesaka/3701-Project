using UnityEngine;
using System.Collections;

public class AdvancedLightFlicker : MonoBehaviour
{
    public Light flickerLight;
    public AudioSource buzzSound;
    public ParticleSystem sparkParticles;
    
    [Header("Main Settings")]
    public float normalIntensity = 1f;
    public float chanceToFlicker = 0.3f;
    
    [Header("Flicker Settings")]
    public float minFlickerDuration = 0.5f;
    public float maxFlickerDuration = 2f;
    public float minDelayBetweenFlickers = 2f;
    public float maxDelayBetweenFlickers = 10f;
    
    [Header("Sound Settings")]
    public float minPitch = 0.8f;
    public float maxPitch = 1.2f;

    void Start()
    {
        if (flickerLight == null)
            flickerLight = GetComponent<Light>();
        
        StartCoroutine(FlickerRoutine());
    }

    IEnumerator FlickerRoutine()
    {
        while (true)
        {
            if (Random.value < chanceToFlicker)
            {
                float flickerDuration = Random.Range(minFlickerDuration, maxFlickerDuration);
                float endTime = Time.time + flickerDuration;
                
                while (Time.time < endTime)
                {
                    // 随机灯光强度
                    flickerLight.intensity = Random.Range(0f, normalIntensity);
                    
                    // 随机播放声音
                    if (buzzSound != null && !buzzSound.isPlaying && Random.value > 0.5f)
                    {
                        buzzSound.pitch = Random.Range(minPitch, maxPitch);
                        buzzSound.Play();
                    }
                    
                    // 随机触发粒子效果
                    if (sparkParticles != null && Random.value > 0.8f)
                    {
                        sparkParticles.Play();
                    }
                    
                    yield return new WaitForSeconds(Random.Range(0.02f, 0.2f));
                }
                
                // 恢复正常
                flickerLight.intensity = normalIntensity;
            }
            
            yield return new WaitForSeconds(Random.Range(minDelayBetweenFlickers, maxDelayBetweenFlickers));
        }
    }
}