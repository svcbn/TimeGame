using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.currentCheckPoint++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.CameraMove();
            gameObject.GetComponent<Collider>().isTrigger = false;
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            enabled = false;
        }
    }
    
}
