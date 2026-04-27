using UnityEngine;
using UnityEngine.UI;

public class ThrowController : MonoBehaviour
{
    [SerializeField] private Transform _throwPoint;
    [SerializeField] private Transform _guardTarget;
    [SerializeField] Image _hand;
    [SerializeField] Sprite _handSprite;

    private GameObject _currentPrefab;
    private bool _hasItem = false;
    public void Pickup(GameObject prefab)
    {
        _currentPrefab = prefab;
        _hasItem = true;
    }

    private void Update()
    {
        if (_hasItem && Input.GetMouseButtonDown(0))
        {
            Throw();
        }
    }

    private void Throw()
    {
        GameObject obj = Instantiate(_currentPrefab, _throwPoint.position, _throwPoint.rotation);
        _hand.sprite = _handSprite;
        obj.SetActive(true);

        BottleMovement moveScript = obj.GetComponent<BottleMovement>();
        moveScript.SetTarget(_guardTarget);

        _hasItem = false;
        _currentPrefab = null;
    }
}
