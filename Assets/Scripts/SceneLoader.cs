using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public static SceneLoader Instance;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            //DontDestroyOnLoad(gameObject);    
        } 
        else {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName) {        
        SceneManager.LoadScene(sceneName);
    }    

    IEnumerator LoadSceneAsync(string sceneName) {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
