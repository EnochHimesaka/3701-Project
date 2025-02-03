using UnityEngine;

public class LightingSettingsManager : MonoBehaviour
{
    [Header("���� Lighting Settings -> Environment -> Intensity Multiplier")]
    [Range(0f, 2f)] // ���û�������Χ
    [SerializeField]
    public float intensityMultiplier = 1f;

    void Start()
    {
        // ��ʼ�� Lighting �� Intensity Multiplier
        RenderSettings.reflectionIntensity = intensityMultiplier;
    }

    void Update()
    {
        // ������ʱ��̬�޸� Lighting Intensity
        RenderSettings.reflectionIntensity = intensityMultiplier;
    }
}