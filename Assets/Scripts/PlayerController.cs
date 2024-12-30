using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Yatay hareket için input değeri.
    private float _horizontal;
    // Oyuncunun hareket hızı.
    private float _speed = 8f;
    // Zıplama kuvveti.
    private float _jumpPower = 16f;
    // Oyuncunun yüzü sağa dönük mü kontrolü.
    private bool _isFacingRight = true;

    // Oyuncunun Rigidbody2D bileşeni (fizik hareketleri için).
    [SerializeField] private Rigidbody2D _rigidbody;
    // Oyuncunun yerle temasını kontrol etmek için bir nokta.
    [SerializeField] private Transform _groundCheck;
    // Zemin olarak tanımlanan katmanları belirten LayerMask.
    [SerializeField] private LayerMask _groundLayer;

    // AnimatorController referansı.
    private AnimatorController _animatorController;

    // Script çalıştırıldığında bir kez çağrılır, bileşenlerin referanslarını alır.
    private void Awake()
    {
        // AnimatorController'ı çocuk nesnelerden alır.
        _animatorController = GetComponentInChildren<AnimatorController>();
    }

    private void Update()
    {
        // Unity'nin input sistemiyle oyuncunun yatay hareket girdisini alır (A/D veya Sol/ Sağ ok tuşları).
        _horizontal = Input.GetAxisRaw("Horizontal");

        // Zıplama tuşuna basıldığında ve oyuncu zeminde ise yukarı doğru bir hız uygulanır.
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rigidbody.velocity = new(_rigidbody.velocity.x, _jumpPower);
        }

        // Zıplama tuşu bırakıldığında ve oyuncu hala zıplıyorsa yukarı doğru hızı azaltarak kısa zıplama efekti yaratır.
        if (Input.GetButtonUp("Jump") && _rigidbody.velocity.y > 0f)
        {
            _rigidbody.velocity = new(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);
        }

        // Animasyonları kontrol eden fonksiyonu çağırır.
        HandleAnimations();

        // Oyuncunun yönünü giriş değerine göre çevirir (sağa/sola).
        Flip();
    }

    private void FixedUpdate()
    {
        // Oyuncunun hareketini fizik motoru ile gerçekleştirir. 
        // Hızını yatay giriş değerine göre ayarlar, dikey hızını korur.
        _rigidbody.velocity = new(_horizontal * _speed, _rigidbody.velocity.y);
    }

    private bool IsGrounded()
    {
        // Oyuncunun zeminde olup olmadığını kontrol eder.
        // _groundCheck pozisyonunda küçük bir daire oluşturup belirli katmanlarla çakışma olup olmadığını kontrol eder.
        return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
    }

    private void Flip()
    {
        // Oyuncunun yönünü giriş değerine göre değiştirir.
        if (_isFacingRight && _horizontal < 0f || !_isFacingRight && _horizontal > 0f)
        {
            // Oyuncunun yön kontrol değişkenini tersine çevirir.
            _isFacingRight = !_isFacingRight;

            // Ölçeği yatay eksende tersine çevirerek sprite'ı döndürür.
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void HandleAnimations()
    {
        // Animatörün hız parametresini yatay giriş değerine göre ayarlar.
        _animatorController.SetVariable("Speed", _horizontal);

        // Animatörün zıplama parametresini dikey hız değerine göre ayarlar.
        _animatorController.SetVariable("Jump", _rigidbody.velocity.y);

        // Animatörün zeminde olma parametresini zeminde olup olmamasına göre ayarlar.
        _animatorController.SetVariable("IsGrounded", IsGrounded());
    }
}
