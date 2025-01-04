using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Yatay hareket yönü (-1: sola, 1: sağa)
    private float _horizontal = -1;
    // Hareket hızı
    [SerializeField] private float _speed = 5f;
    // Yüzü sağa dönük mü kontrolü
    private bool _isFacingRight = true;

    // Rigidbody bileşeni (fizik tabanlı hareket için)
    [SerializeField] private Rigidbody2D _rigidbody;

    [Space(10)]
    // Ateş etme hızı
    [SerializeField] private float _fireRate;
    // Ateş etme menzili
    [SerializeField] private float _fireRange = 1.5f;

    // Bir sonraki ateş zamanı
    private float _nextFire;

    // AnimatorController bileşeni referansı
    private AnimatorController _animatorController;
    // Hasar verme bileşeni referansı
    private DealDamage _dealDamage;

    // Devriye rotası
    [SerializeField] private PatrolRoute _patrolRourte;

    // Oyuncu rotada mı kontrolü
    private bool _playerInRoute = false;
    // Oyuncu nesnesi referansı
    private GameObject _player;

    // Oyuncu menzilde mi kontrolü
    private bool _playerInRange = false;

    private void Awake()
    {
        // AnimatorController ve DealDamage bileşenlerini al
        _animatorController = GetComponentInChildren<AnimatorController>();
        _dealDamage = GetComponentInChildren<DealDamage>();
    }

    private void Start()
    {
        // Oyuncu rotaya girdiğinde ve çıktığında tetiklenecek olaylara dinleyici ekle
        _patrolRourte.OnPlayerEnter.AddListener(OnPlayerEnter);
        _patrolRourte.OnPlayerExit.AddListener(OnPLayerExit);
    }

    void Update()
    {
        // Eğer oyuncu rotadaysa
        if (_playerInRoute)
        {
            // Oyuncunun konumuna göre yatay hareket yönünü belirle
            var position = transform.position.x - _player.transform.position.x;

            if (position < -_fireRange)
            {
                _horizontal = 1;
                _playerInRange = false;
            }
            else if (position <= 0 && position > -_fireRange && !_playerInRange)
            {
                _horizontal = 0;
                _playerInRange = true;
                _nextFire = Time.time + _fireRate;
            }
            else if (position > _fireRange)
            {
                _horizontal = -1;
                _playerInRange = false;
            }
            else if (position >= 0 && position < _fireRange && !_playerInRange)
            {
                _horizontal = 0;
                _playerInRange = true;
                _nextFire = Time.time + _fireRate;
            }

            // Oyuncu menzilde ve ateş etme zamanı geldiyse
            if (Time.time > _nextFire && (position > -_fireRange && position < _fireRange))
            {
                _nextFire = Time.time + _fireRate;

                // Saldırı animasyonunu başlat ve hasar uygula
                _animatorController.PlayAnimation("Attack");
                _dealDamage.DealDamageInRange();
            }
        }
        else
        {
            // Devriye rotasında hareket yönünü belirle
            if (transform.position.x - _patrolRourte.LeftPoint <= 0.01f)
            {
                _horizontal = 1;
            }
            else if (_patrolRourte.RightPoint - transform.position.x <= 0.01f)
            {
                _horizontal = -1;
            }
        }

        // Animasyonları güncelle
        HandleAnimations();

        // Karakterin yüzünü çevir
        Flip();
    }

    private void FixedUpdate()
    {
        // Fizik tabanlı hareketi uygula (Unity'nin fizik sistemi kullanılarak)
        _rigidbody.linearVelocity = new(_horizontal * _speed, _rigidbody.linearVelocity.y);
    }

    private void Flip()
    {
        // Karakterin yüzünü giriş yönüne göre çevir
        if (_isFacingRight && _horizontal < 0f || !_isFacingRight && _horizontal > 0f)
        {
            _isFacingRight = !_isFacingRight;

            // Karakterin ölçeğini x ekseninde ters çevirerek yönünü değiştir
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void HandleAnimations()
    {
        // Animasyona hız değişkenini ayarla
        _animatorController.SetVariable("Speed", _horizontal);
    }

    private void OnPlayerEnter(GameObject player)
    {
        // Oyuncu rotaya girdiğinde çağrılır
        _playerInRoute = true;
        _player = player;
    }

    private void OnPLayerExit()
    {
        // Oyuncu rotadan çıktığında çağrılır
        _playerInRoute = false;
        _player = null;
    }
}

