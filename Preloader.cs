using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("CoronaGame");
    }

    // Update is called once per frame
    void Update()
    {

    }

}

