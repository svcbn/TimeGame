using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public Door door;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            door.CollectSwitch(gameObject);
            AbilityCircle ac = GameObject.Find("AbilityArea").GetComponent<AbilityCircle>();
            ac.transform.localScale = Vector3.one * ac.maxSize;
        }
    }


}
