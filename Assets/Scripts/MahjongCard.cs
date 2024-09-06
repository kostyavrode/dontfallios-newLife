using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MahjongCard : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private void Awake()
    {

        //Material[] mats = GetComponentInChildren<MeshRenderer>().materials;
        //mats[0].mainTexture = textur;
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }
}
