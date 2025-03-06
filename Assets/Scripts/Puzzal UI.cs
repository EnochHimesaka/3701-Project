using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CircuitManager : MonoBehaviour
{
    public List<GameObject> circuitPieces; // 电路组件
    public GameObject successUI; // 成功 UI
    public GameObject puzzleUI; // 电路谜题 UI
    public Player_PickUP player;

    void Start()
    {
        foreach (GameObject piece in circuitPieces)
        {
            piece.GetComponent<Button>().onClick.AddListener(() => RotatePiece(piece));
        }
    }

    void RotatePiece(GameObject piece)
    {
        piece.transform.Rotate(0, 0, -90); // 旋转电路
        CheckCircuit();
    }

    void CheckCircuit()
    {
        bool allCorrect = true;
        foreach (GameObject piece in circuitPieces)
        {
            if (!piece.CompareTag("Correct")) // 正确状态的电路
            {
                allCorrect = false;
                break;
            }
        }

        if (allCorrect)
        {
            Debug.Log("电路连接成功！");
            successUI.SetActive(true);
            puzzleUI.SetActive(false);
            Time.timeScale = 1; // 恢复游戏
        }
    }
}
