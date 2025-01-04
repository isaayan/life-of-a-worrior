using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    // Maksimum sağlık değeri
    [SerializeField] private float _maxHealth;
    // Mevcut sağlık değeri
    [SerializeField] private float _currentHealth;

    // Nesnenin ait olduğu takım
    [SerializeField] protected int _team;

    // Nesne öldüğünde tetiklenecek olay
    public UnityEvent OnDie;

    private void Awake()  
    {
        // Mevcut sağlık, maksimum sağlığa ayarlanır
        _currentHealth = _maxHealth;
    }

    // Hasar alma işlemi
    public void TakeDamage(float damage)
    {
        // Sağlık değerinden hasar miktarını çıkar
        _currentHealth -= damage;

        // Eğer sağlık sıfırın altına düşerse
        if (_currentHealth <= 0)
        {
            // Ölüm olayını tetikle
            OnDie?.Invoke();

            // Bu bileşeni yok et
            Destroy(this);
        }
    }

    // Nesnenin ait olduğu takımı döndürür
    public int GetTeam()
    {
        return _team;
    }
}

