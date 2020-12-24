using System.Collections;
using System.Collections.Generic;
using OpenCVForUnitySample;
using UnityEngine;

public class CallFfmpeg : MonoBehaviour
{
   [SerializeField] private VideoWriterExample videoWriterExample;

    void Start()
    {
        // var executer = new NativeExecuter();
        // var callbackParameter = new CallbackParameter
        // {
        //     callbackGameObjectName = gameObject.name,
        //     callbackFunctionName = "Callback"
        // };
        // var parameterJson = JsonUtility.ToJson(callbackParameter);
        // executer.Execute("hogeMethod", parameterJson);
    }

    public void CallJavaScriptFfmpegFunction()
    {
        videoWriterExample.SaveMovieToIndexedDB();
        var executer = new NativeExecuter();
        var callbackParameter = new CallbackParameter
        {
            callbackGameObjectName = gameObject.name,
            callbackFunctionName = "Callback",
            callbackMoviePathName = videoWriterExample.indexedDBDataPath
        };
        var parameterJson = JsonUtility.ToJson(callbackParameter);
        //var parameterJson = JsonUtility.ToJson(videoWriterExample.indexedDBDataPath);
        Debug.Log("CallJavaScriptFfmpegFunction: "+videoWriterExample.indexedDBDataPath);
        executer.Execute("FfmpegMethod", parameterJson);
    }

    public void Update()
    {
        // if (Input.GetKeyDown(KeyCode.P))
        // {
        //     var executer = new NativeExecuter();
        //     var callbackParameter = new CallbackParameter
        //     {
        //         callbackGameObjectName = gameObject.name,
        //         callbackFunctionName = "Callback"
        //     };
        //     var parameterJson = JsonUtility.ToJson(callbackParameter);
        //     executer.Execute("hogeMethod", parameterJson);
        // }
        // if (Input.GetKeyDown(KeyCode.O))
        // {
        //     var executer = new NativeExecuter();
        //     var callbackParameter = new CallbackParameter
        //     {
        //         callbackGameObjectName = gameObject.name,
        //         callbackFunctionName = "Callback"
        //     };
        //     var parameterJson = JsonUtility.ToJson(callbackParameter);
        //     executer.Execute("hogeMethod", videoWriterExample.indexedDBDataPath);
        // }
        // if (Input.GetKeyDown(KeyCode.I))
        // {
        //     var executer = new NativeExecuter();
        //     var callbackParameter = new CallbackParameter
        //     {
        //         callbackGameObjectName = gameObject.name,
        //         callbackFunctionName = "Callback"
        //     };
        //     var parameterJson = JsonUtility.ToJson("aiueo");
        //     executer.Execute("FfmpegMethod", parameterJson);
        // }
    }

    public void Callback()
    {
        Debug.Log("callback from js");
    }
}
