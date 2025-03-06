using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CircuitManager : MonoBehaviour
{
    public List<GameObject> circuitPieces; // ��·���
    public GameObject successUI; // �ɹ� UI
    public GameObject puzzleUI; // ��·���� UI
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
        piece.transform.Rotate(0, 0, -90); // ��ת��·
        CheckCircuit();
    }

    void CheckCircuit()
    {
        bool allCorrect = true;
        foreach (GameObject piece in circuitPieces)
        {
            if (!piece.CompareTag("Correct")) // ��ȷ״̬�ĵ�·
            {
                allCorrect = false;
                break;
            }
        }

        if (allCorrect)
        {
            Debug.Log("��·���ӳɹ���");
            successUI.SetActive(true);
            puzzleUI.SetActive(false);
            Time.timeScale = 1; // �ָ���Ϸ
        }
    }
}
