using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private float trackingSpeed=2f;
    [SerializeField] private float offsetZ=3f;
    [SerializeField] private float offsetX = 0.66f;
    [SerializeField] private Transform target;
    private void Update()
    {
        {
            Vector3 tempPosition = new Vector3(target.position.x + offsetX, this.transform.position.y, target.position.z - offsetZ);
            transform.position = Vector3.Lerp(transform.position, tempPosition, trackingSpeed * Time.deltaTime);
        }
    }
}
