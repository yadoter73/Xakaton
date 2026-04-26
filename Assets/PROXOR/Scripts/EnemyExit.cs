using UnityEngine;
using PrimeTween;
using TMPro;

public class EnemyExit : MonoBehaviour
{
    [SerializeField] EnemyFollowing _enemy;
    [SerializeField] GameObject _wall;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] TextMeshProUGUI _textOxrannik;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _textOxrannik.gameObject.SetActive(false);
            _enemy._target = _wall;
            Tween.Delay(1)
                .OnComplete(() =>
                {
                    _text.color = Color.green;
                    _text.gameObject.SetActive(true);
                    _text.text = "Фух, кажется он уходит. Мне срочно надо в клуб к джессике";
                    _textOxrannik.gameObject.SetActive(false);
                    Tween.Delay(4).OnComplete(() => { _text.gameObject.SetActive(false); _textOxrannik.gameObject.SetActive(false); });
                });
            Tween.Delay(5).OnComplete(() => _enemy.gameObject.SetActive(false));
        }
    }
}
