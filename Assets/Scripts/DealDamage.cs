using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DealDamage : MonoBehaviour
{
    // Uygulanacak hasar miktarı
    [SerializeField] private float _damage;

    // Düşman takım kimliği
    [SerializeField] protected int _enemyTeam;

    // Menzildeki düşmanların sağlık denetleyicilerinin listesi
    private List<HealthController> _enemiesInRange = new();

    // Menzildeki tüm düşmanlara hasar uygular
    public void DealDamageInRange()
    {
        foreach (var enemy in _enemiesInRange)
        {
            enemy.TakeDamage(_damage); // Her düşmana hasar verilir
        }
    }

    // Menzile bir nesne girdiğinde tetiklenir
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("testing: " + collision.name); // Giriş yapan nesnenin adını loglar
        var healthController = collision.GetComponent<HealthController>();

        // Eğer HealthController bileşeni yoksa işlem yapılmaz
        if (!healthController) return;

        // Eğer nesne düşman takımına ait değilse işlem yapılmaz
        if (healthController.GetTeam() != _enemyTeam) return;

        // Düşman listeye eklenir
        _enemiesInRange.Add(healthController);
    }

    // Menzilden bir nesne çıktığında tetiklenir
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("testing 2: " + collision.name); // Çıkan nesnenin adını loglar
        // Çıkan nesneyi listede arar
        var index = _enemiesInRange.FindIndex(x => x.GetInstanceID() == collision.gameObject.GetInstanceID());

        // Eğer nesne listede yoksa işlem yapılmaz
        if (index < 0) return;

        // Nesne listeden kaldırılır
        _enemiesInRange.RemoveAt(index);
    }
}
