using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ShowInfo : MonoBehaviour
{
    [SerializeField] GameObject infoCanvas;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    ARRaycastManager raycastManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject canvasInstance = Instantiate(infoCanvas, transform, false);
        //raycastManager = GameObject.Find("AR Camera").GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
