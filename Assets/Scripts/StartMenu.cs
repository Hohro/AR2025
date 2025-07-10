using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartARScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}