using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject mapUI;        // ��ͼUI��壬����һ��Image��Panel
    private bool isMapVisible = false;

    void Start()
    {
        if (mapUI != null)
        {
            mapUI.SetActive(false); // ��ʼ���ص�ͼ
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            isMapVisible = !isMapVisible;
            if (mapUI != null)
            {
                mapUI.SetActive(isMapVisible);
            }
        }
    }
}
