using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float _maxHealth; // Maksimum sağlık değeri
    [SerializeField] private float _currentHealth; // Mevcut sağlık değeri

    [SerializeField] protected int _team; // Karakterin ait olduğu takım

    public UnityEvent OnDie; // Ölüm olayı
    public UnityEvent<float, float> OnTookDamage; // Hasar alındığında tetiklenen olay

    private Animator _animator; // Animator bileşeni
    private bool _isDying = false; // Ölüm animasyonunun birden fazla tetiklenmesini engelleyen bayrak

    private Collider2D _collider; // Karakterin Collider2D bileşeni
    private Rigidbody2D _rb; // Karakterin Rigidbody2D bileşeni

    private void Awake()
    {
        _currentHealth = _maxHealth; // Başlangıçta mevcut sağlık maksimum sağlıkla aynı

        // Animator bileşenini al
        _animator = GetComponentInChildren<Animator>();
        _collider = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();

        if (_animator == null)
        {
            Debug.LogWarning("Animator bileşeni nesnede veya alt nesnelerde bulunamadı.");
        }
    }

    // Hasar alma fonksiyonu
    public void TakeDamage(float damage)
    {
        if (_isDying) return; // Eğer karakter ölüyorsa hasar almaz.

        _currentHealth -= damage; // Sağlık değeri hasar kadar azalır

        OnTookDamage?.Invoke(_currentHealth, _maxHealth); // Hasar alındığında olay tetiklenir

        if (_currentHealth <= 0)
        {
            _isDying = true; // Ölüm durumu aktif edilir

            _rb.bodyType = RigidbodyType2D.Static; // Rigidbody'yi statik hale getirir (hareketsiz yapar)
            _collider.enabled = false; // Collider'ı devre dışı bırakır

            PlayDamageAnimation(); // Hasar animasyonunu oynatır

            // Ölüm animasyonunu geciktirir (Animator Event veya manuel çağırma)
            Invoke(nameof(PlayDeathAnimation), 0.5f); // Gecikme süresi ayarlanabilir
        }
        else
        {
            PlayDamageAnimation(); // Ölüm gerçekleşmediyse hasar animasyonunu oynatır
        }
    }

    // Hasar animasyonunu oynatan fonksiyon
    private void PlayDamageAnimation()
    {
        if (_animator != null)
        {
            _animator.SetTrigger("Damage"); // Hasar trigger'ını tetikler
        }
    }

    // Ölüm animasyonunu oynatan fonksiyon
    private void PlayDeathAnimation()
    {
        if (_animator != null)
        {
            _animator.SetTrigger("Die"); // Ölüm trigger'ını tetikler
        }

        OnDie?.Invoke(); // Ölüm olayını tetikler

        // Ölüm animasyonunun bitmesini beklemek için nesne yok edilir
        Destroy(gameObject, 1.5f); // 1.5 saniye sonra yok edilir
    }

    // Takımı döndüren fonksiyon
    public int GetTeam()
    {
        return _team; // Takım bilgisi döndürülür
    }
}
