using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager_Main : MonoBehaviour
{
    public static Manager_Main Instance { get; private set; }
    public static bool IsSceneLoading = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning($"{this} already exists. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        StartCoroutine(Process_Main());
    }

    private IEnumerator Process_Main()
    {
        yield return new WaitForSeconds(3f);
        GoScene(1);
    }

    public static void GoScene(int Scene_Play)
    {
        if (IsSceneLoading || Instance == null)
        {
            Debug.LogError("Scene loading is already in progress or Manager_Main instance is null.");
            return;
        }
        Instance.StartCoroutine(Instance.Process_GoScene(Scene_Play));
    }

    private IEnumerator Process_GoScene(int Scene_Play)
    {
        IsSceneLoading = true;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(Scene_Play);
        IsSceneLoading = false;
    }
}
