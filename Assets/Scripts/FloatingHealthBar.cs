using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    public static FloatingHealthBar Instance; // Singleton örneği

    [SerializeField] private Slider _slider; // Sağlık barını temsil eden Slider bileşeni

    private void Awake()
    {
        Instance = this; // Singleton örneği, tek bir instance oluşturur
    }

    // Sağlık barını güncelleyen fonksiyon
    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        _slider.value = currentValue / maxValue; // Mevcut sağlık değerini maksimum sağlık değerine oranlayarak slider'ı günceller
    }
}
