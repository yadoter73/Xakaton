using UnityEngine;

public class DeleteAgent : MonoBehaviour
{
    [SerializeField] EnemyFollowing _enemy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _enemy.gameObject.SetActive(false);
        }
    }
}
