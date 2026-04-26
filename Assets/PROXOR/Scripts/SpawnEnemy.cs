using TMPro;
using UnityEngine;
using PrimeTween;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _textObject;
    [SerializeField] private TextMeshProUGUI _uiText;

    public void ActivateEnemySequence()
    {
        _enemyPrefab.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        _textObject.SetActive(true);
        _uiText.text = "Охранник нашел вас!";
        Tween.Delay(3f).OnComplete(() => ActivateEnemySequence());
    }
}
