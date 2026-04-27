using UnityEngine;
using TMPro;
using PrimeTween;
public class StartGame : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] EnemyFollowing _enemy;
    void Start()
    {
        Tween.Delay(4f).OnComplete(() => _enemy.enabled = true);
        Tween.Delay(4f).OnComplete(() => _text.gameObject.SetActive(false));
    }

}
