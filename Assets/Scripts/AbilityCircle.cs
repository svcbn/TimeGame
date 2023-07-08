using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AbilityCircle : MonoBehaviour
{
    public float maxSize = 12f;
    [SerializeField] float scaleSpeed = 10f;
    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localScale = Vector3.zero;
        player = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.localScale.x < maxSize && !player.isSlowMode)
        {
            gameObject.transform.localScale += Vector3.one * Time.deltaTime * scaleSpeed;
        }
        if(gameObject.transform.localScale.x > 0 && player.isSlowMode)
        {
            gameObject.transform.localScale -= Vector3.one * Time.deltaTime * scaleSpeed;
        }
    }

    public void ResetCirecle()
    {
        gameObject.transform.localScale = Vector3.zero;
    }
}
