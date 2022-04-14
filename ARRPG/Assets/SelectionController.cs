using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionController : MonoBehaviour
{
    [SerializeField] private Camera arCam;
    private Vector2 touchPos = default;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPos = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = arCam.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    GameObject obj = hitObject.transform.GetComponent<GameObject>();
                    //if (obj != null) Destroy(obj);
                }
            }
        }
    }
}
