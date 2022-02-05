using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    Rigidbody rb;
    Vector3 jumpForceVector;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        jumpForceVector = new Vector3(0, jumpForce, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Input.GetAxis("Horizontal") * Vector3.right * speed;
        transform.position += Input.GetAxis("Vertical") * Vector3.forward * speed;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(jumpForceVector);
        }
    }
}
