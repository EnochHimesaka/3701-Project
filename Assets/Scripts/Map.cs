using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject mapUI;        // 地图UI面板，例如一个Image或Panel
    private bool isMapVisible = false;

    void Start()
    {
        if (mapUI != null)
        {
            mapUI.SetActive(false); // 初始隐藏地图
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
