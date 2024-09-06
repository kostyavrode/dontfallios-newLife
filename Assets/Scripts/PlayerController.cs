using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using DG.Tweening;
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public static Action onPlayerDeath;
    [SerializeField] private GameObject model;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float yDeathZone=-1f;
    [SerializeField] private float jumpForce = 20f;
    private bool isCanMove;
    public int completedBlocks;
    private void Awake()
    {
        completedBlocks = 1;
        instance = this;
        isCanMove = true;
        if (rigidbody==null)
        {
            rigidbody = GetComponent<Rigidbody>();
        }
        InputController.SwipeEvent += OnSwipe;
    }
    private void OnDisable()
    {
        InputController.SwipeEvent -= OnSwipe;
    }
    private void Update()
    {
        CheckDeath();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Block")
        {
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            completedBlocks += 1;
            
        }
    }
    private void Move(Vector3 direction)
    {
        if (isCanMove)
        {
            isCanMove = false;
            transform.DOJump(transform.position + direction, 1f, 1, 0.5f).OnComplete(ChangeCanMoveState);
            rigidbody.AddForce((Vector3.up) * jumpForce,ForceMode.VelocityChange);
            //transform.TransformDirection(direction+transform.position);
            //transform.Translate(direction+transform.position);
            //Observable.EveryUpdate().TakeWhile(x => !isCanMove).Subscribe(x => rigidbody.MovePosition(transform.position + direction * Time.deltaTime*1));
            //Observable.EveryUpdate().TakeWhile(x => !isCanMove).Subscribe(x => transform.Translate((direction + transform.position)*Time.deltaTime*2));
            //rigidbody.MovePosition(transform.position + direction*Time.deltaTime);
            //rigidbody.velocity =((direction+Vector3.up) * 2.25f);
            //Observable.Timer(System.TimeSpan.FromSeconds(0.5f)).Subscribe(x => ChangeCanMoveState());
            //transform.DOMoveX(transform.position.x + direction.x, 0.5f);
            //transform.DOMoveZ(transform.position.z + direction.z, 0.5f);
            transform.DORotate(RotationCalc(direction), 0.25f);
        }
    }
    private void OnSwipe(Vector2 direction)
    {
        Vector3 dir =
            direction == Vector2.up ? Vector3.forward :
            direction == Vector2.down ? Vector3.back : (Vector3)direction;
        Move(dir);
    }
    private void CheckDeath()
    {
        if (transform.position.y < yDeathZone)
        {
            Death();
        }
    }
    public void Restart()
    {
        completedBlocks = 0;
        transform.position = new Vector3(0, 2.39f, 0);
        isCanMove=true;
        Time.timeScale = 1f;
    }
    private void Death()
    {
        onPlayerDeath?.Invoke();
        //this.enabled = false;
    }
    private Vector3 RotationCalc(Vector3 direction)
    {
        if (direction==Vector3.forward)
        {
            return Vector3.zero;
        }
        if (direction==Vector3.back)
        {
            return new Vector3(0f, 180f, 0f);
        }
        if (direction==Vector3.left)
        {
            return new Vector3(0f, -90f, 0f);
        }
        else
        {
            return new Vector3(0f, 90f, 0f);
        }
    }
    private void ChangeCanMoveState()
    {
        isCanMove = !isCanMove;
    }
}
