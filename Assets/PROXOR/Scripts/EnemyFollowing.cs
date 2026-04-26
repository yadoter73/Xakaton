using UnityEngine;
using UnityEngine.AI;
using PrimeTween;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyFollowing : MonoBehaviour
{
    [SerializeField] GameObject _target;
    private NavMeshAgent _agent;
    private Animator _anim;

    [SerializeField] private string obstacleTag = "Obstacle"; 
    [SerializeField] private float pushForce = 5f;
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponentInChildren<Animator>();
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
            StartCoroutine(deleting(collision.gameObject));
        }
    }
    private IEnumerator deleting(GameObject gameObject)
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        SceneManager.LoadScene("GAMEOVER");
    }
}

