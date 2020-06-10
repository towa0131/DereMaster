using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using LitJson;

public class KeyControl : MonoBehaviour
{
    public bool enableEscape = true;
    // Start is called before the first frame update

    private DeresuteAPI api;
    public void Start()
    {
        /* Sample User */
        string udid = "55531483-9352-9510-7911-743178891239";
        int viewerId = 536218652;
        int userId = 264425958;
        api = new DeresuteAPI(udid, viewerId, userId);
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && enableEscape)
        {
            Quit();
        }
        else if (Input.GetKey(KeyCode.D)) // DEBUG
        {

            Dictionary<string, object> args = new Dictionary<string, object>()
            {
                { "app_type", 0 },
                { "campaign_data", "" },
                { "campaign_user", 1468 },
                { "campaign_sign", Binary.md5("All your APIs are belong to us.") }
            };

            string result = api.Call(args, "/load/check");
            var dic = JsonMapper.ToObject(result);
            Debug.Log(dic["data_headers"]["result_code"]);
        }
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
            UnityEngine.Application.Quit();
        #endif
    }

    public IEnumerator DelayMethod(float waitTime, UnityAction action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }
}
