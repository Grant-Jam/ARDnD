using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoCanvasController : MonoBehaviour
{
    GameObject arCam;
    // Start is called before the first frame update
    void Start()
    {
        arCam = GameObject.Find("AR Camera");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(arCam.transform);
    }
}
