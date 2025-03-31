using UnityEngine;
using UnityEngine.AI;
public class NPCnavmesh : MonoBehaviour {
    private NavMeshAgent navMeshAgent;

    [SerializeField] private Transform movePositionTransform;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update() {
        navMeshAgent.destination = movePositionTransform.position;
    }
}
