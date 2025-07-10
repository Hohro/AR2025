using UnityEngine;

public class PrefabSwitcher : MonoBehaviour
{
    public PlaceOnPlane placer;

    public void NextPrefab()
    {
        placer.NextPrefab();
    }

    public void PreviousPrefab()
    {
        placer.PreviousPrefab();
    }
}