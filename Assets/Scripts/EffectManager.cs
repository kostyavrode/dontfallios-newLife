using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EffectManager : MonoBehaviour
{
    public static Action<Vector3> onPlayEffect;
    [SerializeField] private ParticleSystem blockEffect;
    private void Awake()
    {
        onPlayEffect += EffectInstantiate;
    }
    private void OnDisable()
    {
        onPlayEffect -= EffectInstantiate;
    }
    private void EffectInstantiate(Vector3 position)
    {
        ParticleSystem effect=Instantiate(blockEffect);
        effect.transform.position = position;
        effect.Play();
    }
}
