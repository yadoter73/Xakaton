using UnityEngine;
using UnityEngine.AI;
using PrimeTween;
public class EnemyFollowing : MonoBehaviour
{
    [SerializeField] GameObject _target;
    private NavMeshAgent _agent;

    [SerializeField] private string obstacleTag = "Obstacle"; 
    [SerializeField] private float pushForce = 5f;
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (_target != null)
        {
            _agent.SetDestination(_target.transform.position);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(obstacleTag))
        {
            if (!collision.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb = collision.gameObject.AddComponent<Rigidbody>();
            }

            Vector3 pushDirection = collision.transform.position - transform.position;
            pushDirection.y = 0;
            pushDirection = pushDirection.normalized;

            rb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
    }
}

