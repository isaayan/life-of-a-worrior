using UnityEngine;
using UnityEngine.Events;

public class PatrolRoute : MonoBehaviour
{
    // Devriye rotasının sol sınır noktası
    private float _leftPoint = 0;
    // Devriye rotasının sağ sınır noktası
    private float _rightPoint = 0;

    // Sol sınır noktasını dışarıya yalnızca okunabilir olarak sağlar
    public float LeftPoint => _leftPoint;
    // Sağ sınır noktasını dışarıya yalnızca okunabilir olarak sağlar
    public float RightPoint => _rightPoint;

    // Oyuncu rotaya girdiğinde tetiklenen olay (Oyuncu nesnesiyle birlikte)
    public UnityEvent<GameObject> OnPlayerEnter;
    // Oyuncu rotadan çıktığında tetiklenen olay
    public UnityEvent OnPlayerExit;

    private void Awake()
    {
        // Sol ve sağ sınır noktalarını devriye rotasının genişliğine göre hesaplar
        _leftPoint = transform.position.x - (transform.localScale.x / 2);
        _rightPoint = transform.position.x + (transform.localScale.x / 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Eğer rotaya giren nesne "Player" etiketi taşıyorsa
        if (collision.tag == "Player")
        {
            // Oyuncu rotaya giriş olayını tetikle
            OnPlayerEnter?.Invoke(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Eğer rotadan çıkan nesne "Player" etiketi taşıyorsa
        if (collision.tag == "Player")
        {
            // Oyuncu rotadan çıkış olayını tetikle
            OnPlayerExit?.Invoke();
        }
    }
}
