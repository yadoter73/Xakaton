using UnityEngine;

public class StopEnemy : MonoBehaviour
{
    [SerializeField] EnemyFollowing _enemy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _enemy.enabled = false;
            _enemy.gameObject.SetActive(false);
        }
    }
}
