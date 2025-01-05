using UnityEngine;
using UnityEngine.Events;

public class PatrolRoute : MonoBehaviour
{
    // Sol ve sağ sınır noktalarını tutan değişkenler
    private float _leftPoint = 0;
    private float _rightPoint = 0;

    // Sol ve sağ sınır noktalarına erişim sağlayan özellikler
    public float LeftPoint => _leftPoint;
    public float RightPoint => _rightPoint;

    // Oyuncu giriş ve çıkışlarını dinleyen olaylar
    public UnityEvent<GameObject> OnPlayerEnter;
    public UnityEvent OnPlayerExit;

    // Nesne oluşturulurken sol ve sağ sınır noktalarını hesaplar
    private void Awake()
    {
        _leftPoint = transform.position.x - (transform.localScale.x / 2); // Sol sınır noktası
        _rightPoint = transform.position.x + (transform.localScale.x / 2); // Sağ sınır noktası
    }

    // Oyuncu collider'ı bu alanın içine girdiğinde tetiklenir
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") // Eğer çarpan nesne "Player" etiketine sahipse
        {
            OnPlayerEnter?.Invoke(collision.gameObject); // Oyuncu girdi olayını tetikle
        }
    }

    // Oyuncu collider'ı bu alanın dışına çıktığında tetiklenir
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") // Eğer çarpan nesne "Player" etiketine sahipse
        {
            OnPlayerExit?.Invoke(); // Oyuncu çıkış olayını tetikle
        }
    }
}
