using UnityEngine;
using UnityEngine.SceneManagement;
public class WallKiller : MonoBehaviour
{
    private static int _wallCount = 0;
    private void Awake() => _wallCount = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _wallCount++;
            if (_wallCount >= 2)
            {
                _wallCount = 0;
                SceneManager.LoadScene("GAMEOVER");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _wallCount--;
            if (_wallCount < 0) _wallCount = 0;
        }
    }
}