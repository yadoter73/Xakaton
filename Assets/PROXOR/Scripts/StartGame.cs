using UnityEngine;
using PrimeTween;
public class StartGame : MonoBehaviour
{

    [SerializeField] GameObject _text;
    [SerializeField] EnemyFollowing _enemyFollowing;
    private void Start()
    {
        _text.SetActive(true);
        Tween.Delay(3f).OnComplete(() => _enemyFollowing.enabled = true);
        _text.SetActive(false);
    }
}
