using UnityEngine;

public class ModeSwitcher : MonoBehaviour
{
    public bool markerMode = false;
    public PlaceOnPlane placer;

    public void ToggleMode()
    {
        markerMode = !markerMode;
        if (markerMode)
        {
            // Alle platzierten Objekte zerstören
            foreach (var obj in GameObject.FindGameObjectsWithTag("PlacedObject"))
            {
                Destroy(obj);
            }
        }
    }
}
