using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{
    // Karakterin yatay hareket girişi
    protected float _horizontal;

    // Karakterin hareket hızı
    [SerializeField] protected float _speed;

    // Karakterin hangi yöne baktığını kontrol eden değişken
    protected bool _isFacingRight = true;

    // Karakterin fiziksel hareketlerini kontrol eden Rigidbody2D bileşeni
    [SerializeField] protected Rigidbody2D _rigidbody;

    [Space(10)]
    // Ateş etme hızı
    [SerializeField] protected float _fireRate;

    // Bir sonraki ateş etme zamanı
    protected float _nextFire;

    // AnimatorController bileşenine referans
    protected AnimatorController _animatorController;

    // Hasar verme sistemine referans
    protected DealDamage _dealDamage;

    // Sağlık kontrolüne referans
    protected HealthController _healthController;

    // Nesne oluşturulurken gerekli bileşenlere referans alır
    protected virtual void Awake()
    {
        _animatorController = GetComponentInChildren<AnimatorController>();
        _dealDamage = GetComponentInChildren<DealDamage>();

        // HealthController bileşenine OnDie olayını dinlemek için abone olur
        _healthController = GetComponent<HealthController>();
        if (_healthController != null)
        {
            _healthController.OnDie.AddListener(Die);
        }
    }

    // Her karede animasyonları ve yön değiştirmeyi işler
    protected virtual void Update()
    {
        HandleAnimations(); // Animasyonları işleme
        Flip(); // Karakterin yönünü değiştirme
    }

    // Fizik güncellemeleri için kullanılır, hareket işlemleri burada yapılır
    protected virtual void FixedUpdate()
    {
        // Hareketi fizik sistemi aracılığıyla işler
        _rigidbody.linearVelocity = new Vector2(_horizontal * _speed, _rigidbody.linearVelocity.y);
    }

    // Karakterin yüzünü girişe göre döndüren yöntem
    protected void Flip()
    {
        // Girişe göre karakteri sağa veya sola çevirir
        if (_isFacingRight && _horizontal < 0f || !_isFacingRight && _horizontal > 0f)
        {
            _isFacingRight = !_isFacingRight;

            // Karakterin ölçeğini çevirerek görüntüsünü döndürür
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    // Animasyonları işleyecek olan soyut yöntem, türetilmiş sınıflarda uygulanır
    protected abstract void HandleAnimations();

    /// <summary>
    /// Karakterin ölümünü işleyen yöntem. Türetilmiş sınıflarda özelleştirilebilir.
    /// </summary>
    protected virtual void Die()
    {
        // Karakterin hareket ve lojik işlemlerini devre dışı bırakır
        _rigidbody.linearVelocity = Vector2.zero;
        enabled = false;

        // Ölüm animasyonunu oynatır (varsa)
        _animatorController?.PlayAnimation("Die");

        // Oyun nesnesini yok etme veya ölüm işlemlerini yönetme
        Destroy(gameObject, 2f); // Ölüm animasyonunun tamamlanması için gecikme
    }
}
