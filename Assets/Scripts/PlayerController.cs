using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Yatay hareket için giriş değeri
    private float _horizontal;
    // Oyuncunun hareket hızı
    private float _speed = 8f;
    // Zıplama gücü
    private float _jumpPower = 16f;
    // Oyuncunun hangi yöne baktığını kontrol eder (sağa mı sola mı)
    private bool _isFacingRight = true;

    // Oyuncunun fiziksel hareketini kontrol etmek için Rigidbody2D referansı
    [SerializeField] private Rigidbody2D _rigidbody;
    // Oyuncunun yere değip değmediğini kontrol etmek için kullanılan nokta
    [SerializeField] private Transform _groundCheck;
    // Yere temas kontrolü için kullanılan katman
    [SerializeField] private LayerMask _groundLayer;

    [Space(10)]
    // Ateş etme sıklığını belirler
    [SerializeField] private float _fireRate;

    // Bir sonraki ateş etme zamanı
    private float _nextFire;

    // Animator ve hasar verme bileşenleri için referanslar
    private AnimatorController _animatorController;
    private DealDamage _dealDamage;

    private void Awake()
    {
        // AnimatorController ve DealDamage bileşenlerini bulur
        _animatorController = GetComponentInChildren<AnimatorController>();
        _dealDamage = GetComponentInChildren<DealDamage>();
    }

    private void Update()
    {
        // Unity'nin giriş sisteminden yatay hareket bilgisini alır (A/D veya Sol/Sağ ok tuşları)
        _horizontal = Input.GetAxisRaw("Horizontal");

        // Zıplama tuşuna basıldığında ve oyuncu yerdeyse, yukarı doğru bir hız uygular
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rigidbody.linearVelocity = new(_rigidbody.linearVelocity.x, _jumpPower);
        }

        // Zıplama tuşu bırakıldığında ve oyuncu hala yerdeyse, yukarı hızını azaltarak kısa bir zıplama efekti oluşturur
        if (Input.GetButtonUp("Jump") && _rigidbody.linearVelocity.y > 0f)
        {
            _rigidbody.linearVelocity = new(_rigidbody.linearVelocity.x, _rigidbody.linearVelocity.y * 0.5f);
        }

        // Ateş etme tuşuna (F) basıldığında, oyuncu yerdeyse ve ateş etme için bekleme süresi geçmişse
        if (Input.GetKeyDown(KeyCode.F) && IsGrounded() && Time.time > _nextFire)
        {
            // Bir sonraki ateş etme zamanını günceller
            _nextFire = Time.time + _fireRate;

            // Saldırı animasyonunu oynatır
            _animatorController.PlayAnimation("Attack");

            // Yakındaki düşmanlara hasar verir
            _dealDamage.DealDamageInRange();
        }

        // Animasyonları günceller
        HandleAnimations();

        // Oyuncunun yönünü çevirir
        Flip();
    }

    private void FixedUpdate()
    {
        // Hareketi Unity'nin fizik sistemi aracılığıyla işler. Hız, nesnenin hızına göre ayarlanır
        _rigidbody.linearVelocity = new(_horizontal * _speed, _rigidbody.linearVelocity.y);
    }

    private bool IsGrounded()
    {
        // Oyuncunun yere temas edip etmediğini kontrol eder
        // _groundCheck pozisyonunda, belirli bir yarıçapta ve _groundLayer katmanında bir çarpışma olup olmadığını kontrol eder
        return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
    }

    private void Flip()
    {
        // Oyuncunun sağa veya sola dönmesini sağlar
        if (_isFacingRight && _horizontal < 0f || !_isFacingRight && _horizontal > 0f)
        {
            _isFacingRight = !_isFacingRight;

            // Oyuncunun yatay ölçeğini -1 ile çarparak yönünü ters çevirir
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void HandleAnimations()
    {
        // Yürüme animasyonunu kontrol etmek için "Speed" değişkenini günceller
        _animatorController.SetVariable("Speed", _horizontal);

        // Zıplama animasyonunu kontrol etmek için "Jump" değişkenini günceller
        _animatorController.SetVariable("Jump", _rigidbody.linearVelocity.y);

        // Yerde olma durumunu kontrol etmek için "IsGrounded" değişkenini günceller
        _animatorController.SetVariable("IsGrounded", IsGrounded());
    }
}
