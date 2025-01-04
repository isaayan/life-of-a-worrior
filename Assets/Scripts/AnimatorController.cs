using UnityEngine;

// Bu bileşenin bir Animator bileşeni gerektirdiğini belirtir.
[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour
{
    // Animator bileşeni için bir referans
    private Animator _animator;

    private void Awake()
    {
        // Aynı nesnedeki Animator bileşenini alıyoruz.
        _animator = GetComponent<Animator>();
    }

    // Belirtilen bir animasyonu oynatır.
    public void PlayAnimation(string animationName)
    {
        // Animator bileşenindeki belirtilen animasyon klibini çalıştırır.
        _animator.Play($"Base Layer.{animationName}");
    }

    // Float türündeki bir Animator değişkenini ayarlar.
    public void SetVariable(string variableName, float variableValue)
    {
        // Belirtilen değişken adını ve değerini Animator'a gönderir.
        _animator.SetFloat(variableName, variableValue);
    }

    // Integer türündeki bir Animator değişkenini ayarlar.
    public void SetVariable(string variableName, int variableValue)
    {
        // Belirtilen değişken adını ve değerini Animator'a gönderir.
        _animator.SetInteger(variableName, variableValue);
    }

    // Boolean türündeki bir Animator değişkenini ayarlar.
    public void SetVariable(string variableName, bool variableValue)
    {
        // Belirtilen değişken adını ve değerini Animator'a gönderir.
        _animator.SetBool(variableName, variableValue);
    }
}
