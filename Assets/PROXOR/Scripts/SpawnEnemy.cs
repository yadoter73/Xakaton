using TMPro;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _textObject;
    [SerializeField] private TextMeshProUGUI _uiText;

    public void ActivateEnemySequence()
    {
        Instantiate(_enemyPrefab, _spawnPoint.position, _spawnPoint.rotation);

        _textObject.SetActive(true);
        _uiText.text = "Охранник нашел вас!";
    }
    private void OnTriggerEnter(Collider other)
    {
        ActivateEnemySequence();
    }
}
