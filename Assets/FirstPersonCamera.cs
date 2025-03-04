using UnityEngine;
using Cinemachine;

public class FirstPersonCamera : MonoBehaviour
{
    private Camera mainCamera;
    private CinemachineVirtualCamera cinemachineCamera;

    public float firstPersonFOV = 60f; // 第一人称 FOV
    public Transform firstPersonPosition; // 角色的第一人称位置

    void Start()
    {
        // 获取 Camera 组件
        mainCamera = GetComponent<Camera>();

        // 获取 Cinemachine Virtual Camera（如果存在）
        cinemachineCamera = GetComponent<CinemachineVirtualCamera>();

        // 检查是否找到相机
        if (mainCamera == null)
        {
            Debug.LogError("未找到 Camera 组件，请确保该脚本挂在 MainCamera 上！");
        }

        if (cinemachineCamera != null)
        {
            Debug.Log("Cinemachine Virtual Camera Detected, Modifying FOV.");
            cinemachineCamera.m_Lens.FieldOfView = firstPersonFOV; // 直接修改 Cinemachine FOV
        }
        else if (mainCamera != null)
        {
            Debug.Log("Standard Camera Detected, Modifying FOV.");
            mainCamera.fieldOfView = firstPersonFOV; // 修改普通 Camera FOV
        }
    }

    void Update()
    {
        // 如果 `firstPersonPosition` 存在，则将相机位置设置到第一人称视角
        if (firstPersonPosition != null)
        {
            transform.position = firstPersonPosition.position;
            transform.rotation = firstPersonPosition.rotation;
        }
    }
}