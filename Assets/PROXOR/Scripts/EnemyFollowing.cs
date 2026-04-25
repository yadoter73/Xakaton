using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowing : MonoBehaviour
{
    [SerializeField] GameObject _target;
    private NavMeshAgent _agent;
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    public void Following()
    {
        _agent.SetDestination(_target.transform.position);
    }
}
