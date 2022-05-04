using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class MultipleTrackedImagesManager : MonoBehaviour
{

    [SerializeField] private GameObject[] arObjectsToPlace;

    [SerializeField] private Vector3 scaleFactor = new Vector3(0.1f,0.1f,0.1f);

    //private bool terVis = false;

    [SerializeField] private Text imageTrackedText;
    [SerializeField] private Text imageTrackedText2;

    private ARTrackedImageManager trackedImageManager;

    private Dictionary<string, GameObject> arObjects = new Dictionary<string, GameObject>();

    private bool locked = false;

    private void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();

        foreach(GameObject arObject in arObjectsToPlace)
        {
            GameObject newARObject = Instantiate(arObject, Vector3.zero, Quaternion.identity);
            newARObject.name = arObject.name;
            newARObject.SetActive(false);
            arObjects.Add(arObject.name, newARObject);
        }
    }

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {

        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateARImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateARImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            arObjects[trackedImage.referenceImage.name].SetActive(false);
        }
    }

    private void UpdateARImage(ARTrackedImage trackedImage)
    {
        imageTrackedText.text = trackedImage.referenceImage.name;

        AssignGameObject(trackedImage.referenceImage.name, trackedImage.transform.position, trackedImage.transform.rotation);
    }

    void AssignGameObject(string name, Vector3 newPosition, Quaternion newRotation)
    {
        if(arObjectsToPlace != null)
        {
            if (Vector3.Distance(arObjects[name].transform.position, Camera.current.transform.position) < 0.1)
            {
                arObjects[name].SetActive(false);
            }

            // Find a way to lock object positions in world space when not being tracked

            else
            {
                if (arObjects[name].tag != "Environment")
                {
                    if (locked == false)
                    {
                        arObjects[name].SetActive(true);
                        arObjects[name].transform.localScale = scaleFactor / 5;
                        arObjects[name].transform.position = newPosition;
                        arObjects[name].transform.rotation = newRotation;
                    }
                }
                else
                {
                    /*if (terVis == false)
                      {
                          arObjects[name].transform.position = newPosition;
                          terVis = true;
                      }*/
                    if (locked == false)
                    {
                        arObjects[name].transform.position = newPosition;
                        arObjects[name].SetActive(true);
                        arObjects[name].transform.localScale = scaleFactor / 15;
                    }
                }

            }

            foreach (GameObject go in arObjects.Values)
            {
                if (Vector3.Distance(go.transform.position, Camera.current.transform.position) < 0.15)
                {
                    go.SetActive(false);
                }

            }

        }
    }

    public void LockToggle()
    {
        if(locked == true)
        {
            locked = false;
            imageTrackedText2.text = "Unlocked";
        }
        else
        {
            locked = true;
            imageTrackedText2.text = "Locked";
        }
    }

}
