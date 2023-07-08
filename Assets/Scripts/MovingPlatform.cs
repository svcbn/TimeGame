using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject platform;
    public GameObject wayPoint1;
    public GameObject wayPoint2;
    Vector3 dir;
    [SerializeField] bool goDirection = true;
    [SerializeField] float platformSpeed = 0.8f;
    [SerializeField] float dist = 1.05f;
    LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        platform.transform.localPosition = new Vector3(wayPoint1.transform.localPosition.x, wayPoint1.transform.localPosition.y, 0);

        DrawLine();
    }

    // Update is called once per frame
    void Update()
    {
        if(goDirection)
        {
            dir = wayPoint2.transform.localPosition - platform.transform.localPosition;
            dir.z = 0;
            platform.transform.localPosition += platformSpeed * Time.deltaTime * dir;
        }
        else
        {
            dir = wayPoint1.transform.localPosition - platform.transform.localPosition;
            dir.z = 0;
            platform.transform.localPosition += platformSpeed * Time.deltaTime * dir;
        }

        if (Vector3.Distance(wayPoint2.transform.localPosition, platform.transform.localPosition) < dist)
        {
            goDirection = false;
        }
        else if(Vector3.Distance(wayPoint1.transform.localPosition, platform.transform.localPosition) < dist)
        {
            goDirection = true;
        }
    }

    void DrawLine()
    {
        lr = GetComponent<LineRenderer>();
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;

        lr.SetPosition(0, wayPoint1.transform.position);
        lr.SetPosition(1, wayPoint2.transform.position);
    }
}
