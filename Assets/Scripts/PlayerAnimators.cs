using UnityEngine;

public class PlayerAnimators : MonoBehaviour
{
    // Farklı karakterler için animatör denetleyicileri
    [SerializeField] public RuntimeAnimatorController _hero1;
    [SerializeField] public RuntimeAnimatorController _hero2;
    [SerializeField] public RuntimeAnimatorController _king1;
    [SerializeField] public RuntimeAnimatorController _king2;

    // Animator bileşeni
    private Animator _animator;

    // Nesne oluşturulurken karakter tercihini alır ve animatörü ayarlar
    private void Awake()
    {
        var hero = PlayerPrefs.GetInt("hero"); // Seçilen karakterin indeksini PlayerPrefs'ten alır

        _animator = GetComponent<Animator>(); // Animator bileşenini alır

        // Seçilen karaktere göre animatörü ayarlar
        if (hero == 0)
        {
            _animator.runtimeAnimatorController = _hero1; // İlk kahramanın animatörünü kullanır
        }
        else if (hero == 1)
        {
            _animator.runtimeAnimatorController = _hero2; // İkinci kahramanın animatörünü kullanır
        }
        else if (hero == 2)
        {
            _animator.runtimeAnimatorController = _king1; // İlk kralın animatörünü kullanır
        }
        else if (hero == 3)
        {
            _animator.runtimeAnimatorController = _king2; // İkinci kralın animatörünü kullanır
        }
    }
}
