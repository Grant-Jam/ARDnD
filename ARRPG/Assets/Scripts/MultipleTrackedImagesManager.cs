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

    [SerializeField] private Text imageTrackedText;

    private ARTrackedImageManager trackedImageManager;

    private Dictionary<string, GameObject> arObjects = new Dictionary<string, GameObject>();

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

    //private void Dismiss() => welcomePanel.SetActive(false);

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
            arObjects[trackedImage.name].SetActive(false);
        }
    }

    private void UpdateARImage(ARTrackedImage trackedImage)
    {
        imageTrackedText.text = trackedImage.referenceImage.name;

        AssignGameObject(trackedImage.referenceImage.name, trackedImage.transform.position);

    }

    void AssignGameObject(string name, Vector3 newPosition)
    {
        if(arObjectsToPlace != null)
        {
            arObjects[name].SetActive(true);
            arObjects[name].transform.position = newPosition;
            arObjects[name].transform.localScale = scaleFactor;
            foreach(GameObject go in arObjects.Values)
            {
                if(go.name != name)
                {
                    go.SetActive(false);
                }

            }
        }
    }

}
