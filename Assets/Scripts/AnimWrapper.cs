using UnityEngine;

public class LoopAnimation : MonoBehaviour
{
    public Animation anim;

    void Start()
    {
        // Убедитесь, что анимация зациклена
        anim.wrapMode = WrapMode.Loop;

        // Проиграть анимацию
        anim.Play();
    }
}