using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public List<GameObject> switches = new List<GameObject>();
    public GameObject door;
    public GameObject switch1;
    public GameObject switch2;

    // Start is called before the first frame update
    void Start()
    {
        ResetSwitch();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.state == GameManager.GameState.Die)
        {
            ResetSwitch();
        }
        if(switches.Count == 0)
        {
            OpenDoor();
        }
    }

    public void CollectSwitch(GameObject obj)
    {
        switches.Remove(obj);
        obj.SetActive(false);
    }

    void OpenDoor()
    {
        door.SetActive(false);
    }

    void ResetSwitch()
    {
        switches.Clear();
        switches.Add(switch1);
        switches.Add(switch2);
        switch1.SetActive(true);
        switch2.SetActive(true);
        door.SetActive(true);
    }
}
