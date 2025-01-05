using Unity.Cinemachine;
using UnityEngine;

public class CameraShakeManager : MonoBehaviour
{
    // CameraShakeManager sınıfının tekil örneğini tutar
    public static CameraShakeManager Instance;

    // Kamera sarsıntısı için global bir güç değeri
    [SerializeField] private float _gloabalShakeForce = 1f;

    private void Awake()
    {
        // Bu sınıfın tekil örneğini ayarlar
        Instance = this;
    }

    // Belirtilen impulse kaynağı ile kamera sarsıntısı oluşturma yöntemi
    public void CameraShake(CinemachineImpulseSource impulseSource)
    {
        // Verilen impulse kaynağı üzerinden belirtilen güç ile sarsıntı oluşturur
        impulseSource.GenerateImpulseWithForce(_gloabalShakeForce);
    }
}

