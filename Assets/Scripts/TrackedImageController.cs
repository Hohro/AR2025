using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;

public class TrackedImageController : MonoBehaviour
{
    private ARTrackedImageManager trackedImageManager;
    public GameObject markerMemoryPrefab;

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
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
        foreach (var trackedImage in eventArgs.added)
        {
            SpawnMarkerMemory(trackedImage);
        }
    }

    void SpawnMarkerMemory(ARTrackedImage trackedImage)
    {
        GameObject obj = Instantiate(markerMemoryPrefab, trackedImage.transform.position, trackedImage.transform.rotation);
        obj.tag = "MarkerMemory";
    }
}
