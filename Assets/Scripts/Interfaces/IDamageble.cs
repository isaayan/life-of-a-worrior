using UnityEditor;
using UnityEngine;

// IDamageble adında bir arayüz tanımlanıyor.
// Bu arayüz, hasar alabilen (damageable) bir nesnenin sahip olması gereken davranışı belirler.
public interface IDamageble
{
    // Hasar alma işlemi için bir yöntem (metot) tanımlanıyor.
    // Bu yöntem, hasar miktarını (damage) parametre olarak alır.
    public void TakeDamage(float damage)
    {
        // Bu yöntem varsayılan olarak boş bırakılmıştır.
        // Hasar alma işlemi, bu arayüzü uygulayan sınıflar tarafından özelleştirilecektir.
    }
}

