using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class PlayerController : MonoBehaviour
{

    float horizontalInput;
    Vector3 direction;
    Rigidbody playerRb;
    [SerializeField] float speed = 35f;
    [SerializeField] float maxSpeed = 12f;
    [SerializeField] float jumpPower = 10f;
    [SerializeField] bool isGround = false;
    public bool isSlowMode = false;
    [SerializeField] float timeScale = 0.2f;
    public GameObject teleportPoint;
    AbilityCircle abilityCircle;
    [SerializeField] float teleportPower = 50f;

    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        teleportPoint.SetActive(false);
        abilityCircle = GetComponentInChildren<AbilityCircle>();
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        direction = new Vector3(horizontalInput, 0, 0);

        if (playerRb.velocity.x < maxSpeed && playerRb.velocity.x > -maxSpeed)
        {
            if (isGround)
            {
                playerRb.AddForce(direction * speed / 2);

            }
            else
            {
                playerRb.AddForce(direction * speed / 8);

            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
        if(Input.GetMouseButtonDown(0) && !isSlowMode)
        {
            StartCoroutine(SlowMode());
        }
    }

    void Jump()
    {
        playerRb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        isGround = false;
    }

    IEnumerator SlowMode()
    {
        isSlowMode = true;
        Time.timeScale = timeScale;
        teleportPoint.SetActive(true);

        while(isSlowMode)
        {
            if(Input.GetMouseButtonUp(0))
            {
                AbilityRay();
                isSlowMode = false;
                Time.timeScale = 1f;
                teleportPoint.SetActive(false);
                break;
            }
            yield return null;
        }
    }

    void AbilityRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.gameObject.CompareTag("AbilityArea"))
            {
                Teleport();
                abilityCircle.ResetCirecle();
            }
        }       
    }
    

    void Teleport()
    {
        Vector3 dir = teleportPoint.transform.position - transform.position;
        dir.z = 0f;
        transform.position = new Vector3(teleportPoint.transform.position.x, teleportPoint.transform.position.y, 0);
        playerRb.AddForce(dir * teleportPower);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
}
