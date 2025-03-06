using UnityEngine;
using UnityEngine.EventSystems;

public class WireConnector : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public LineRenderer lineRenderer;
    public Transform startPoint;
    private Transform endPoint;
    private ElectricalBoxPuzzle puzzleManager;

    void Start()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
        puzzleManager = FindObjectOfType<ElectricalBoxPuzzle>(); // 获取电路解谜管理类
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, startPoint.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        lineRenderer.SetPosition(1, Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 10)));
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject hitObject = eventData.pointerEnter;
        if (hitObject != null && hitObject.CompareTag("EndPoint"))
        {
            endPoint = hitObject.transform;
            lineRenderer.SetPosition(1, endPoint.position);
            CheckCircuitCompletion();
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    void CheckCircuitCompletion()
    {
        if (endPoint != null && endPoint.name == "CorrectEndPoint")
        {
            Debug.Log("电路连接成功！");
            puzzleManager.CompletePuzzle(); // 通知电箱解谜管理器
        }
    }
}
