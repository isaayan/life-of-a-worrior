using UnityEngine;

// GameObject'in bir Animator bileşeni içermesini zorunlu kılar
[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour
{
    private Animator _animator; // Animator bileşenine referans

    private void Awake()
    {
        // Aynı GameObject'ten Animator bileşenini alır
        _animator = GetComponent<Animator>();
    }

    // Belirtilen animasyonu adını kullanarak oynatma yöntemi
    public void PlayAnimation(string animationName)
    {
        _animator.Play($"Base Layer.{animationName}");
    }

    // Animator'daki bir float değişkenini ayarlama yöntemi
    public void SetVariable(string variableName, float variableValue)
    {
        _animator.SetFloat(variableName, variableValue);
    }

    // Animator'daki bir integer değişkenini ayarlama yöntemi
    public void SetVariable(string variableName, int variableValue)
    {
        _animator.SetInteger(variableName, variableValue);
    }

    // Animator'daki bir boolean değişkenini ayarlama yöntemi
    public void SetVariable(string variableName, bool variableValue)
    {
        _animator.SetBool(variableName, variableValue);
    }

    // Belirtilen animasyonun süresini döndüren yöntem
    public float GetAnimationLength(string animationName)
    {
        // Animator'un çalışma zamanındaki kontrolöründeki animasyon kliplerini döngüyle kontrol eder
        foreach (AnimationClip clip in _animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == animationName) // Eğer klip adı verilen animasyon adıyla eşleşirse
            {
                return clip.length; // Klip süresini döndürür
            }
        }

        // Eğer animasyon bulunamazsa bir uyarı mesajı gösterir
        Debug.LogWarning($"Animasyon {animationName} Animator'da bulunamadı.");
        return 0f; // Animasyon bulunamazsa 0 döner
    }
}
