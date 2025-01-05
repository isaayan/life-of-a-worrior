using UnityEngine;

public class EnemyController : CharacterController
{
    [SerializeField] private float _fireRange = 1.5f; // Düşmanın ateş etme mesafesi
    [SerializeField] private PatrolRoute _patrolRoute; // Düşmanın gezinti yolu

    private bool _playerInRoute = false; // Oyuncu gezinti yoluna girdi mi?
    private GameObject _player; // Oyuncu nesnesi
    private bool _playerInRange = false; // Oyuncu ateş menzilinde mi?
    private bool _isTakingDamage = false; // Düşman hasar alırken hareketsiz mi?

    // Awake fonksiyonu, düşman nesnesi başlatıldığında çalışır
    protected override void Awake()
    {
        base.Awake(); // Taban sınıfın Awake fonksiyonunu çağırır

        _patrolRoute.OnPlayerEnter.AddListener(OnPlayerEnter); // Oyuncu gezinti yoluna girdiğinde tetiklenecek olay
        _patrolRoute.OnPlayerExit.AddListener(OnPlayerExit); // Oyuncu gezinti yolundan çıktığında tetiklenecek olay
    }

    // Update fonksiyonu, her frame'de sürekli olarak çağrılır
    protected override void Update()
    {
        if (_isTakingDamage) return; // Eğer düşman hasar alıyorsa hareket etmeyi durdur

        if (_playerInRoute) // Eğer oyuncu gezinti yoluna girdiyse
        {
            float position = transform.position.x - _player.transform.position.x; // Düşman ve oyuncu arasındaki mesafe

            if (position < -_fireRange) // Eğer oyuncu ateş menzilinin solundaysa
            {
                _horizontal = 1; // Düşmanı sağa hareket ettir
                _playerInRange = false;
            }
            else if (position > _fireRange) // Eğer oyuncu ateş menzilinin sağındaysa
            {
                _horizontal = -1; // Düşmanı sola hareket ettir
                _playerInRange = false;
            }
            else if (!_playerInRange) // Eğer oyuncu ateş menzilindeyse
            {
                _horizontal = 0; // Düşmanı durdur
                _playerInRange = true;
                _nextFire = Time.time + _fireRate; // Yeni ateş zamanı ayarlanır
            }

            if (Time.time > _nextFire && position > -_fireRange && position < _fireRange) // Eğer ateş etme zamanı geldiyse
            {
                _nextFire = Time.time + _fireRate; // Yeni ateş zamanı ayarlanır

                _animatorController.PlayAnimation("Attack"); // Saldırı animasyonu oynatılır

                // Hasar kaydının animasyon ile eşleşmesi için gecikme başlatılır
                StartCoroutine(DelayDamage());
            }
        }
        else // Eğer oyuncu gezinti yolunda değilse
        {
            // Düşman, gezinti yolunun sol ya da sağ sınırına yaklaşırsa yön değiştirilir
            if (transform.position.x - _patrolRoute.LeftPoint <= 0.01f)
            {
                _horizontal = 1; // Düşmanı sağa hareket ettir
            }
            else if (_patrolRoute.RightPoint - transform.position.x <= 0.01f)
            {
                _horizontal = -1; // Düşmanı sola hareket ettir
            }
        }

        base.Update(); // Taban sınıfın Update fonksiyonunu çağırır
    }

    // Animasyonları kontrol etmek için kullanılan fonksiyon
    protected override void HandleAnimations()
    {
        _animatorController.SetVariable("Speed", _horizontal); // Düşmanın hareket hızını animasyona yansıtır
    }

    // Oyuncu gezinti yoluna girdiğinde çağrılır
    private void OnPlayerEnter(GameObject player)
    {
        _playerInRoute = true; // Oyuncu gezinti yoluna girdi
        _player = player; // Oyuncu nesnesi kaydedilir
    }

    // Oyuncu gezinti yolundan çıktığında çağrılır
    private void OnPlayerExit()
    {
        _playerInRoute = false; // Oyuncu gezinti yolundan çıktı
        _player = null; // Oyuncu nesnesi sıfırlanır
    }

    // Saldırı hasarını uygulamadan önce gecikme ekleyen fonksiyon
    private System.Collections.IEnumerator DelayDamage()
    {
        // Düşmanı sersemlet
        _isTakingDamage = true;
        _horizontal = 0; // Düşmanı durdur

        // Saldırı animasyonu süresi kadar gecikme eklenir
        yield return new WaitForSeconds(0.3f); // Örnek: 0.3 saniye gecikme
        _dealDamage.DealDamageInRange(); // Menzildeki hedeflere hasar verilir

        // Saldırı animasyonu bitene kadar beklenir
        float attackAnimationTime = _animatorController.GetAnimationLength("Attack"); // Animasyon uzunluğu alınır
        yield return new WaitForSeconds(attackAnimationTime); // Animasyon süresi kadar beklenir

        _isTakingDamage = false; // Hasar almayı bitir
    }

    // Düşman öldüğünde çağrılır
    protected override void Die()
    {
        base.Die(); // Taban sınıfın Die fonksiyonunu çağırır

        // Oyun bitme durumu kontrol edilir ve gereken işlemler yapılır
        if (WinGame.Instance)
        {
            WinGame.Instance.EnemyDied(); // Düşman öldü, oyun ilerletilir
        }
        if (WinEndGame.Instance)
        {
            WinEndGame.Instance.EnemyDied(); // Düşman öldü, oyun bitirilir
        }
    }
}
