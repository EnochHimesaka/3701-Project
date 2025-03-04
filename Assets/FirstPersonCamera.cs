using UnityEngine;
using Cinemachine;

public class FirstPersonCamera : MonoBehaviour
{
    private Camera mainCamera;
    private CinemachineVirtualCamera cinemachineCamera;

    public float firstPersonFOV = 60f; // ��һ�˳� FOV
    public Transform firstPersonPosition; // ��ɫ�ĵ�һ�˳�λ��

    void Start()
    {
        // ��ȡ Camera ���
        mainCamera = GetComponent<Camera>();

        // ��ȡ Cinemachine Virtual Camera��������ڣ�
        cinemachineCamera = GetComponent<CinemachineVirtualCamera>();

        // ����Ƿ��ҵ����
        if (mainCamera == null)
        {
            Debug.LogError("δ�ҵ� Camera �������ȷ���ýű����� MainCamera �ϣ�");
        }

        if (cinemachineCamera != null)
        {
            Debug.Log("Cinemachine Virtual Camera Detected, Modifying FOV.");
            cinemachineCamera.m_Lens.FieldOfView = firstPersonFOV; // ֱ���޸� Cinemachine FOV
        }
        else if (mainCamera != null)
        {
            Debug.Log("Standard Camera Detected, Modifying FOV.");
            mainCamera.fieldOfView = firstPersonFOV; // �޸���ͨ Camera FOV
        }
    }

    void Update()
    {
        // ��� `firstPersonPosition` ���ڣ������λ�����õ���һ�˳��ӽ�
        if (firstPersonPosition != null)
        {
            transform.position = firstPersonPosition.position;
            transform.rotation = firstPersonPosition.rotation;
        }
    }
}