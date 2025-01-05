using EasyTransition;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    // WinGame sınıfının tekil örneği
    public static WinGame Instance;

    // Sahne yükleme işlemleri için DemoLoadScene bileşeni
    [SerializeField] private DemoLoadScene _demoLoadScene;

    // Toplam düşman sayısı
    [SerializeField] private int _totalEnemyCount;

    // Yenilen düşman sayısı
    private int _defetedEnemyCount;

    // Nesne oluşturulurken tekil örneği ayarlar
    private void Awake()
    {
        Instance = this;
    }

    // Düşmanla çarpışıldığında tetiklenir
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Çarpışan nesne oyuncu değilse işlem yapılmaz
        if (collision.tag != "Player") return;

        // Yenilen tüm düşman sayısı, toplam düşman sayısına eşitse Level2 sahnesini yükler
        if (_defetedEnemyCount >= _totalEnemyCount)
        {
            _demoLoadScene.LoadScene("Level2");
        }
    }

    // Bir düşman öldüğünde çağrılır
    public void EnemyDied()
    {
        _defetedEnemyCount++; // Yenilen düşman sayısını bir artırır
    }
}
