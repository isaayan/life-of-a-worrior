using UnityEngine;

public class HeroSelecterManager : MonoBehaviour
{
    // Karakter seçiminde hangi karakterin seçildiğini belirten GameObject'ler
    [SerializeField] private GameObject _hero1Selected;
    [SerializeField] private GameObject _hero2Selected;
    [SerializeField] private GameObject _hero3Selected;
    [SerializeField] private GameObject _hero4Selected;

    // Nesne oluşturulurken ilk karakteri seçer
    private void Awake()
    {
        SelectHero1(); // İlk karakteri seç
    }

    // Karakter 1'i seçme işlemi
    public void SelectHero1()
    {
        _hero1Selected.SetActive(true); // Karakter 1 aktif hale getirilir
        _hero2Selected.SetActive(false); // Karakter 2 devre dışı bırakılır
        _hero3Selected.SetActive(false); // Karakter 3 devre dışı bırakılır
        _hero4Selected.SetActive(false); // Karakter 4 devre dışı bırakılır

        PlayerPrefs.SetInt("hero", 0); // Seçilen karakteri PlayerPrefs'e kaydeder
    }

    // Karakter 2'yi seçme işlemi
    public void SelectHero2()
    {
        _hero2Selected.SetActive(true); // Karakter 2 aktif hale getirilir
        _hero3Selected.SetActive(false); // Karakter 3 devre dışı bırakılır
        _hero4Selected.SetActive(false); // Karakter 4 devre dışı bırakılır
        _hero1Selected.SetActive(false); // Karakter 1 devre dışı bırakılır

        PlayerPrefs.SetInt("hero", 1); // Seçilen karakteri PlayerPrefs'e kaydeder
    }

    // Karakter 3'ü seçme işlemi
    public void SelectHero3()
    {
        _hero3Selected.SetActive(true); // Karakter 3 aktif hale getirilir
        _hero4Selected.SetActive(false); // Karakter 4 devre dışı bırakılır
        _hero1Selected.SetActive(false); // Karakter 1 devre dışı bırakılır
        _hero2Selected.SetActive(false); // Karakter 2 devre dışı bırakılır

        PlayerPrefs.SetInt("hero", 2); // Seçilen karakteri PlayerPrefs'e kaydeder
    }

    // Karakter 4'ü seçme işlemi
    public void SelectHero4()
    {
        _hero4Selected.SetActive(true); // Karakter 4 aktif hale getirilir
        _hero1Selected.SetActive(false); // Karakter 1 devre dışı bırakılır
        _hero2Selected.SetActive(false); // Karakter 2 devre dışı bırakılır
        _hero3Selected.SetActive(false); // Karakter 3 devre dışı bırakılır

        PlayerPrefs.SetInt("hero", 3); // Seçilen karakteri PlayerPrefs'e kaydeder
    }
}
