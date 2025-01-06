using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager_Main : MonoBehaviour
{
    static public Manager_Main Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Debug.LogError($"{this} should be singleton.");
            Destroy(gameObject);
        }
    }
    bool _isDone = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Process_Main());
    }

    IEnumerator Process_Main()
    {
        yield return new WaitForSeconds(3f);
        GoScene(1);
    }


    public static void GoScene(int _sceneIndex)
    {
        SceneManager.LoadScene(_sceneIndex);
    }  

    // Update is called once per frame
    void Update()
    {

    }
}
