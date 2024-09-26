using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollection : MonoBehaviour
{
    UIManager ui;

    void Start()
    {
        ui = UIManager.instance;
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            ui.ScoreUpdate();
            col.transform.GetChild(2).GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
    }
}
