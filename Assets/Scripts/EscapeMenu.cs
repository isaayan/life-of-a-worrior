using UnityEngine;

public class EscapeMenu : WindowController
{
    // Awake fonksiyonu, pencereyi başlatır ve temel açma/kapama işlevini miras alır
    protected override void Awake()
    {
        base.Awake(); // Ana pencereyi kapatma işlemi burada yapılır
    }

    // Update fonksiyonu, her frame'de çağrılır ve Escape tuşuna basılmasını kontrol eder
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Escape tuşuna basıldığında
        {
            if (_isWindowOpen) // Eğer pencere açık ise
            {
                CloseWindow(); // Pencereyi kapat
            }
            else // Eğer pencere kapalı ise
            {
                OpenWindow(); // Pencereyi aç
            }
        }
    }
}
