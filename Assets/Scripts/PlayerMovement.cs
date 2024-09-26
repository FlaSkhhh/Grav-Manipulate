using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;

    [SerializeField] Transform cam;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;

    [SerializeField] GameManger gm;

    bool isGrounded;

    Vector3 velocity;

    float speed = 60f;
    float deathCounter = 0f;
    float vel;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        GroundCheck();

        Vector3 movement = GetInput();
        Move(movement);
        Jump();
        FallControl();
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.05f, groundMask);
        if (isGrounded)
        {
            deathCounter = 0;
            velocity = Vector3.zero;
            anim.SetBool("Falling", false);
        }

    }

    Vector3 GetInput()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        return input;
    }

    void Move(Vector3 movement)
    {
        if (movement.magnitude > 0)
        {
            float lookAngle = Mathf.Atan2(movement.normalized.x, movement.normalized.z) * Mathf.Rad2Deg + cam.localEulerAngles.y;
            transform.GetChild(0).localRotation = Quaternion.Euler(0f, lookAngle, 0f);
            if (transform.localEulerAngles.x == 0 && transform.localEulerAngles.y == 180 && transform.localEulerAngles.z == 180)
            {
                Vector3 movDir = Quaternion.Euler(0f, lookAngle, 0f) * -transform.forward;
                rb.AddRelativeForce(movDir.normalized * speed * Time.deltaTime, ForceMode.VelocityChange);
            }
            else if (transform.localEulerAngles.x!=0)//different movement because up is the new forward for front and back rotation
            {
                Vector3 movDir = Quaternion.Euler(0f, lookAngle, 0f) * transform.up;
                rb.AddRelativeForce(movDir.normalized * speed * Time.deltaTime, ForceMode.VelocityChange);
            }
            else if (transform.localEulerAngles.x == 270)
            {
                Vector3 movDir = Quaternion.Euler(0f, lookAngle, 0f) * -transform.up;
                rb.AddRelativeForce(movDir.normalized * speed * Time.deltaTime, ForceMode.VelocityChange);
            }
            else if (transform.localEulerAngles.x == 0)
            {
                Vector3 movDir = Quaternion.Euler(0f, lookAngle, 0f) * transform.forward;
                rb.AddRelativeForce(movDir.normalized * speed * Time.deltaTime, ForceMode.VelocityChange);
            }

            if (isGrounded) anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            anim.SetTrigger("Jump");
            rb.velocity+=(transform.up*40f);
        }
    }

    void FallControl()
    {
        if (!isGrounded)
        {
            velocity += transform.up* -100f*Time.deltaTime;

            rb.velocity += (velocity * Time.deltaTime); //2 times multiplication by Time.deltaTime for accelration units
            anim.SetBool("Falling", true);

            deathCounter += Time.deltaTime;
            if (deathCounter > 10)
            {
                gm.TimeOver(); //game losing condition if you dont touch anything for 10 seconds
            }
        }
    }
}
