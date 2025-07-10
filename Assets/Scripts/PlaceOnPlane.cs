using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class PlaceOnPlane : MonoBehaviour
{
    public List<GameObject> objectPrefabs;
    private int currentIndex = 0;
    private ARRaycastManager raycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public ModeSwitcher modeSwitcher;

    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        if (raycastManager == null)
        {
            Debug.LogError("ARRaycastManager fehlt!");
        }
    }

    void Update()
    {
        if (!modeSwitcher.markerMode && Input.GetMouseButtonDown(0))
        {
            Vector2 screenPosition = Input.mousePosition;
            if (raycastManager.Raycast(screenPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                GameObject obj = Instantiate(objectPrefabs[currentIndex], hitPose.position, hitPose.rotation);
                obj.tag = "PlacedObject";
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

    public int GetCurrentIndex()
    {
        return currentIndex;
    }
}
