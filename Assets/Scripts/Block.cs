using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;

public class Block : MonoBehaviour, IColor,ITemporary
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private float timeForDeathGoDown = 2f;
    [SerializeField] private float timeForScaleChanging = 1f;
    private Vector3 startScale;
    private bool isFirstStep;
    private void Awake()
    {
        isFirstStep = true;
        startScale = transform.localScale;
        transform.localScale = this.transform.localScale / 10;
        StartScaleChanging();
    }
    public void ApplyColor(Color color)
    {
        meshRenderer.material.color = color;
    }
    public void SetDeathTime(float time)
    {
        try
        {
            Observable.Timer(System.TimeSpan.FromSeconds(time)).TakeUntilDestroy(gameObject).Subscribe(x => DeathGoDown());
        }
        catch
        { }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isFirstStep)
            {
                EffectManager.onPlayEffect?.Invoke(transform.position);
                UIController.onScoreChange?.Invoke();
                isFirstStep = false;
            }
        }
    }
    private void StartScaleChanging()
    {
        transform.DOScale(startScale, timeForScaleChanging);
    }
    private void DeathGoDown()
    {
        var color = meshRenderer.material.color;
        transform.DOMoveY(-10, timeForDeathGoDown).OnComplete(DestroyObject);
        Observable.Interval(System.TimeSpan.FromSeconds(1)).TakeUntilDestroy(gameObject).Subscribe(x =>
        {
            color.a=50;
            gameObject.GetComponent<Renderer>().material.color = color;
        });
    }
    private void DestroyObject()
    {
        Destroy(gameObject);
    }

}
