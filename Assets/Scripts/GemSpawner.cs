using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    public GameObject gemPrefab;
    public GameObject gem;
    private void Awake()
    {
        int temp = Random.Range(0, 5);
        switch(temp)
            {
            case 0:
                return;
            case 1:
                return;
            case 2:
                //GameObject newObj = Instantiate(gemPrefab);
                //newObj.transform.position = this.transform.position;
                gem.SetActive(true);
                break;
            default:
                return;
        }
    }
}
