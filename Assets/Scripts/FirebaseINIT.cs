using Firebase.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirebaseINIT : MonoBehaviour
{
    public static bool firebaseReady;

    void Awake()
    {
        CheckIfReady();
    }

    void CheckIfReady()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            Firebase.DependencyStatus dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                Firebase.FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;
                firebaseReady = true;
                Debug.Log("Firebase is ready for use.");
                LoadNextScene();
            }
            else
            {
                firebaseReady = false;
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
