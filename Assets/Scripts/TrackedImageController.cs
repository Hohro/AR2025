using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class TrackedImageController : MonoBehaviour
{
    private ARTrackedImageManager trackedImageManager;
    public List<GameObject> objectPrefabs;
    private int currentIndex = 0;
    private GameObject spawnedObject;

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
            SpawnPrefab(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            if (trackedImage.trackingState == TrackingState.Tracking && spawnedObject != null)
            {
                spawnedObject.SetActive(true);
                spawnedObject.transform.position = trackedImage.transform.position;
                spawnedObject.transform.rotation = trackedImage.transform.rotation;
            }
            else if (spawnedObject != null)
            {
                spawnedObject.SetActive(false);
            }
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            if (spawnedObject != null)
            {
                Destroy(spawnedObject);
            }
        }
    }

    void SpawnPrefab(ARTrackedImage trackedImage)
    {
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
        }

        spawnedObject = Instantiate(objectPrefabs[currentIndex], trackedImage.transform.position, trackedImage.transform.rotation);
    }

    public void NextPrefab()
    {
        currentIndex = (currentIndex + 1) % objectPrefabs.Count;
        UpdateSpawnedPrefab();
    }

    public void PreviousPrefab()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = objectPrefabs.Count - 1;
        UpdateSpawnedPrefab();
    }

    void UpdateSpawnedPrefab()
    {
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
            spawnedObject = null;
        }
    }
}
