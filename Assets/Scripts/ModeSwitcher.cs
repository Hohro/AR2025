using UnityEngine;

public class ModeSwitcher : MonoBehaviour
{
    public bool markerMode = false;
    public PlaceOnPlane placer;

    public void ToggleMode()
    {
        markerMode = !markerMode;
        Debug.Log("Marker Mode: " + markerMode);

        // Alle PlacedObject löschen
        foreach (var obj in GameObject.FindGameObjectsWithTag("PlacedObject"))
        {
            Destroy(obj);
        }

        if (markerMode)
        {
            SpawnOnAllMarkerMemory(placer.GetCurrentIndex());
        }
    }

    public void NextPrefab()
    {
        if (markerMode)
        {
            foreach (var obj in GameObject.FindGameObjectsWithTag("PlacedObject"))
            {
                Destroy(obj);
            }
            placer.NextPrefab();
            SpawnOnAllMarkerMemory(placer.GetCurrentIndex());
        }
        else
        {
            placer.NextPrefab();
        }
    }

    public void PreviousPrefab()
    {
        if (markerMode)
        {
            foreach (var obj in GameObject.FindGameObjectsWithTag("PlacedObject"))
            {
                Destroy(obj);
            }
            placer.PreviousPrefab();
            SpawnOnAllMarkerMemory(placer.GetCurrentIndex());
        }
        else
        {
            placer.PreviousPrefab();
        }
    }

    void SpawnOnAllMarkerMemory(int prefabIndex)
    {
        var markers = GameObject.FindGameObjectsWithTag("MarkerMemory");
        foreach (var marker in markers)
        {
            GameObject obj = Instantiate(placer.objectPrefabs[prefabIndex], marker.transform.position, marker.transform.rotation);
            obj.tag = "PlacedObject";
        }
    }
}
