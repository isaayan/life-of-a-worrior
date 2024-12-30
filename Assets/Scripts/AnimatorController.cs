using UnityEngine;

// Bu script için GameObject'te Animator bileşeni olduğunu belirtiyor.
[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour
{
    
    private Animator _animator;

    // Script yüklendiğinde çalıştırılır.
    // Animator referansını başlatır.
    private void Awake()
    {  
        //  GameObject'en  Animator bileşenini alır.
        _animator = GetComponent<Animator>();
    }

    // Animasyon adında  bir public oluşturuyoruz.
    public void PlayAnimation(string animationName)
    {
        // "Base Layer"  animasyonunu oynatır.
        _animator.Play($"Base Layer.{animationName}");
    }

    // Float tipindeki parametrelerinin  animasyonlarını kontrol eder.
    public void SetVariable(string variableName, float variableValue)
    {
        
        _animator.SetFloat(variableName, variableValue);
    }

    //  İnteger tipindeki parametrelerin animasyonlarını kontrol eder.
    public void SetVariable(string variableName, int variableValue)
    {
        
        _animator.SetInteger(variableName, variableValue);
    }

    // Beoolean tipindeki parametrelerin animasyonlarını kontrol eder.
    public void SetVariable(string variableName, bool variableValue)
    {
        _animator.SetBool(variableName, variableValue);
    }
}
