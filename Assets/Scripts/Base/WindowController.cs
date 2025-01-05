using UnityEngine;

public abstract class WindowController : MonoBehaviour
{
    // Pencereyi temsil eden menü Canvas'ı
    [SerializeField] private GameObject _menuCanvas;

    // Pencerenin açık olup olmadığını kontrol eden değişken
    protected bool _isWindowOpen = false;

    // Nesne oluşturulurken pencereyi kapatma işlemini yapar
    protected virtual void Awake()
    {
        CloseWindow();
    }

    // Pencereyi açan yöntem
    public virtual void OpenWindow()
    {
        if (_menuCanvas != null) // Eğer menü Canvas'ı atanmışsa
        {
            _menuCanvas.SetActive(true); // Canvas'ı aktif hale getir
        }

        _isWindowOpen = true; // Pencereyi açık olarak işaretle
        Time.timeScale = 0f; // Zamanı durdur
    }

    // Pencereyi kapatan yöntem
    public virtual void CloseWindow()
    {
        if (_menuCanvas != null) // Eğer menü Canvas'ı atanmışsa
        {
            _menuCanvas.SetActive(false); // Canvas'ı deaktif hale getir
        }

        _isWindowOpen = false; // Pencereyi kapalı olarak işaretle
        Time.timeScale = 1f; // Zamanı normale döndür
    }
}
