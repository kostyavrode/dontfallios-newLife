using UnityEngine;

public class LoopAnimation : MonoBehaviour
{
    public Animation anim;

    void Start()
    {
        // ���������, ��� �������� ���������
        anim.wrapMode = WrapMode.Loop;

        // ��������� ��������
        anim.Play();
    }
}