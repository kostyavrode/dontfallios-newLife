using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    [SerializeField] private Camera camera;
    [SerializeField] private Vector3[] cameraPositions;
    [SerializeField] private Vector3[] cameraRotations;
    private void Awake()
    {
        instance = this;
        if (camera.transform.position!=cameraPositions[0])
        {
            camera.transform.position = cameraPositions[0];
        }
        MoveCameraToStart();
    }
    public void MoveCameraToShop()
    {
        camera.transform.DOMove(cameraPositions[0], 1f);
        camera.transform.DORotate(cameraRotations[0], 1f);
        camera.GetComponent<CameraFollower>().enabled = false;
    }
    public void MoveCameraToStart()
    {
        camera.transform.DOMove(cameraPositions[2], 1f);
        camera.transform.DORotate(cameraRotations[2], 1f);
        camera.GetComponent<CameraFollower>().enabled = false;
    }
    public void MoveCameraToGame()
    {
        camera.transform.DOMove(cameraPositions[1], 1f);
        camera.transform.DORotate(cameraRotations[1], 1f);
        camera.GetComponent<CameraFollower>().enabled = true;
    }
}
