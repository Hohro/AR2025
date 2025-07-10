using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using System.Collections;

public class PlaceOnPlane : MonoBehaviour
{
    public List<GameObject> objectPrefabs;
    private int currentIndex = 0;
    private ARRaycastManager raycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed left-click.");
            Vector2 screenPosition = Input.mousePosition;

            if (raycastManager.Raycast(screenPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                Instantiate(objectPrefabs[currentIndex], hitPose.position, hitPose.rotation);
            }
        }
    }

    public void NextPrefab()
    {
        currentIndex = (currentIndex + 1) % objectPrefabs.Count;
    }

    public void PreviousPrefab()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = objectPrefabs.Count - 1;
    }

}
