using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class LoadMenu : MonoBehaviour
{
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        if(File.Exists(Application.persistentDataPath + "/account.dat"))
        {
            StartCoroutine("LoadScene", "Menu");
        }
        else
        {
            GameObject panel = (GameObject)Resources.Load("Prefab/Popup/Account Create");
            GameObject obj = Instantiate(panel, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            obj.transform.SetParent(canvas.transform, false);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadScene(string level)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(level);
        async.allowSceneActivation = false;
        
        while(async.progress < 0.9f)
        {
            Debug.Log(async.progress);
            yield return null;
        }

        yield return new WaitForSeconds(3);
        GameObject sound = GameObject.Find("Sounds");
        if(sound != null){
            GameObject.Destroy(sound);
        }
 
        async.allowSceneActivation = true;
    }
}
