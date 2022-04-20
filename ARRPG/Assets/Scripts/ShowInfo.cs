using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ShowInfo : MonoBehaviour
{
    [SerializeField] GameObject infoCanvas;
    [SerializeField] float hideDistance;
    private GameObject canvasInstance;
    private Transform arCamTransform;

    // Start is called before the first frame update
    void Start()
    {
        canvasInstance = Instantiate(infoCanvas, transform, false);
        canvasInstance.SetActive(false);
        arCamTransform = GameObject.Find("AR Camera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        ToggleInfo();
    }

    public void ToggleInfo()
    {
        float distanceToCam = Vector3.Distance(this.transform.position, arCamTransform.position);
        Debug.Log("distance: " + distanceToCam);
        if (distanceToCam < hideDistance)
        {
            canvasInstance.SetActive(true);
        }
        else canvasInstance.SetActive(false);
    }
}
