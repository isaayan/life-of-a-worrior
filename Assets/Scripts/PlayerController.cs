using Unity.Cinemachine;
using UnityEngine;

public class PlayerController : CharacterController
{
    // Zıplama gücü
    private float _jumpPower = 16f;

    // Yere temas kontrolü için yer belirleyici
    [SerializeField] private Transform _groundCheck;

    // Yerin katman maskesi
    [SerializeField] private LayerMask _groundLayer;

    // Oyuncu hasar aldığında donmuş olup olmadığını takip eden değişken
    private bool _isTakingDamage = false; // Stun olup olmadığını takip eder

    // Hasar verme durumu
    private bool _dealedDamage = false;

    // Cinemachine impuls kaynağı
    private CinemachineImpulseSource _impulseSource;

    // Nesne oluşturulurken gerekli bileşenleri alır
    protected override void Awake()
    {
        base.Awake();

        _impulseSource = GetComponent<CinemachineImpulseSource>();

        // Eğer HealthController varsa, hasar aldığında olay dinleyicisi ekler
        if (_healthController != null)
        {
            _healthController.OnTookDamage.AddListener(OnTookDamage);
        }
    }

    // Her karede inputları kontrol eder ve hareketleri işler
    protected override void Update()
    {
        // Eğer oyuncu hasar alıyorsa, diğer hareketleri durdurur
        if (_isTakingDamage) return; // Stun olduğunda tüm aksiyonları durdur

        _horizontal = Input.GetAxisRaw("Horizontal"); // Yatay hareket girişi

        // Zıplama butonuna basıldığında ve yerle temas halinde zıplama işlemi yapılır
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _jumpPower);
        }

        // Zıplama butonu bırakıldığında yukarı hareketin hızını azaltır
        if (Input.GetButtonUp("Jump") && _rigidbody.linearVelocity.y > 0f)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _rigidbody.linearVelocity.y * 0.5f);
        }

        // F tuşuna basıldığında, yere basılıysa ve atış için zaman uygunsa
        if (Input.GetKeyDown(KeyCode.F) && IsGrounded() && Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate; // Bir sonraki ateş etme zamanı

            _animatorController.PlayAnimation("Attack"); // Saldırı animasyonunu oynatır

            _dealedDamage = false; // Hasar henüz verilmedi

            // Hasar kaydını animasyonla uyumlu olacak şekilde geciktirir
            StartCoroutine(DelayDamage());
        }

        base.Update(); // Parent sınıfın Update metodunu çağırır
    }

    // Yerin üzerinde olup olmadığını kontrol eder
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
    }

    // Animasyonlarla ilgili değişkenleri ayarlayan metod
    protected override void HandleAnimations()
    {
        _animatorController.SetVariable("Speed", _horizontal); // Yatay hız
        _animatorController.SetVariable("Jump", _rigidbody.linearVelocity.y); // Zıplama hızı
        _animatorController.SetVariable("IsGrounded", IsGrounded()); // Yerde olup olmadığını kontrol eder
    }

    // Hasar verme işlemini geciktiren Coroutine
    private System.Collections.IEnumerator DelayDamage()
    {
        // Oyuncuyu stunlar
        _isTakingDamage = true;
        _horizontal = 0; // Yatay hareketi durdurur

        // Saldırı animasyonunun zamanlamasına göre gecikme (animasyona uyacak şekilde ayarlanabilir)
        yield return new WaitForSeconds(0.15f); // Örnek: 0.3 saniye gecikme

        if (!_dealedDamage)
        {
            _dealDamage.DealDamageInRange(); // Hasar verir
            _dealedDamage = true; // Hasar verildiğini işaretler
        }

        // Saldırı animasyonunun uzunluğuna göre bekler
        float attackAnimationTime = _animatorController.GetAnimationLength("Attack"); // Bu metodun var olduğundan emin ol
        yield return new WaitForSeconds(attackAnimationTime);

        _isTakingDamage = false; // Stun durumunu sonlandırır
    }

    // Hasar alındığında çağrılır
    public void OnTookDamage(float health, float maxHealth)
    {
        FloatingHealthBar.Instance.UpdateHealthBar(health, maxHealth); // Sağlık çubuğunu günceller

        CameraShakeManager.Instance.CameraShake(_impulseSource); // Kamera sarsıntısı uygular
    }
}
