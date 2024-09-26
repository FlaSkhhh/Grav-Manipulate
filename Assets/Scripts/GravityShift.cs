using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityShift : MonoBehaviour
{
    public Transform holo;
    Vector3 forward;

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            holo.gameObject.SetActive (true);
            holo.position = transform.position + transform.right/2.7f; //offset because idle anim has dude leaning left
            holo.rotation = transform.rotation * Quaternion.Euler(0,0,90);
            
            if (Input.GetKeyDown(KeyCode.Return)) 
            {
                transform.Rotate(0,0, + 90);
                Invoke("CorrectDecimals", 1f);
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            holo.gameObject.SetActive(true);
            holo.position = transform.position + -transform.right/3;
            holo.rotation = transform.rotation * Quaternion.Euler(0, 0, -90);

            if (Input.GetKeyDown(KeyCode.Return))
            {
                transform.Rotate(0, 0, -90);
                Invoke("CorrectDecimals", 1f);
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            holo.gameObject.SetActive(true);
            holo.position = transform.position + transform.forward/3;
            holo.rotation = transform.rotation * Quaternion.Euler(-90, 0,0 );

            if (Input.GetKeyDown(KeyCode.Return))
            {
                transform.Rotate(-90, 0,0);
                Invoke("CorrectDecimals", 1f);
            }
        } else if (Input.GetKey(KeyCode.DownArrow))
        {
            holo.gameObject.SetActive(true);
            holo.position = transform.position + -transform.forward/3;
            holo.rotation = transform.rotation * Quaternion.Euler(90, 0, 0 );

            if (Input.GetKeyDown(KeyCode.Return))
            {
                transform.Rotate(+90,0, 0);
                Invoke("CorrectDecimals", 1f);
            }
        }
        else
        {
            holo.gameObject.SetActive (false);
        }
    }

    void CorrectDecimals()
    {
        transform.rotation=Quaternion.Euler(Mathf.Round(transform.localEulerAngles.x / 10) * 10, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
}
