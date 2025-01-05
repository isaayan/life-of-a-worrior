using EasyTransition;
using UnityEngine;
using UnityEngine.Events;

public class WinEndGame : MonoBehaviour
{
    public static WinEndGame Instance;

    [SerializeField] private UnityEvent OnGameEnd;
    [SerializeField] private int _totalEnemyCount;

    private int _defetedEnemyCount;

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;

        if (_defetedEnemyCount >= _totalEnemyCount)
        {
            OnGameEnd?.Invoke();
        }
    }

    public void EnemyDied()
    {
        _defetedEnemyCount++;
    }
}
