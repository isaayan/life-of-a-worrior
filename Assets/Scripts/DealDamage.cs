using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class DealDamage : MonoBehaviour
{
    // Verilecek hasar miktarı
    [SerializeField] private float _damage;

    // Düşman takım numarası
    [SerializeField] protected int _enemyTeam;

    // Hasar menzilindeki düşmanları tutan liste
    [SerializeField] private List<HealthController> _enemiesInRange = new();

    // Hasar menzilindeki düşmanlara hasar verme yöntemi
    public void DealDamageInRange()
    {
        // Eğer menzil içinde düşman varsa hasar uygular
        if (_enemiesInRange != null && _enemiesInRange.Count > 0)
        {
            foreach (var enemy in _enemiesInRange.ToList()) // Listeyi dolaş
            {
                if (enemy) // Eğer düşman nesnesi geçerliyse
                {
                    if (enemy.GetTeam() != _enemyTeam) continue; // Eğer düşman takımda değilse atla

                    enemy.TakeDamage(_damage); // Hasar uygula
                }
            }
        }
    }

    // Düşman menzile girdiğinde tetiklenir
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("testing: " + collision.name); // Test için çarpışma bilgisi yazdır

        // Çarpışan nesneden HealthController bileşenini al
        var healthController = collision.GetComponent<HealthController>();

        if (!healthController) return; // Eğer HealthController yoksa işlemi sonlandır

        if (healthController.GetTeam() != _enemyTeam) return; // Eğer düşman takımda değilse işlemi sonlandır

        // Düşmanı menzil listesine ekle
        _enemiesInRange.Add(healthController);
    }

    // Düşman menzilden çıktığında tetiklenir
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("testing 2: " + collision.name); // Test için çarpışma bilgisi yazdır

        // Çarpışan nesneden HealthController bileşenini al
        var healthController = collision.GetComponent<HealthController>();

        if (!healthController) return; // Eğer HealthController yoksa işlemi sonlandır

        // Menzil listesindeki düşmanı bul
        var index = _enemiesInRange.FindIndex(x => x.GetInstanceID() == healthController.GetInstanceID());

        if (index < 0) return; // Eğer düşman bulunamazsa işlemi sonlandır

        // Düşmanı menzil listesinden kaldır
        _enemiesInRange.RemoveAt(index);
    }
}
