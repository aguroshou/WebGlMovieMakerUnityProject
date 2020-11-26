 using System;
 using UnityEngine;
 using System.Collections;
 [Serializable] public class CallbackParameter : MonoBehaviour
 {
     public string callbackGameObjectName;
     public string callbackFunctionName;

     void Start()
     {
         var executer = new NativeExecuter();
         var callbackParameter = new CallbackParameter
         {
             callbackGameObjectName = gameObject.name,
             callbackFunctionName = "Callback"
         };
         var parameterJson = JsonUtility.ToJson(callbackParameter);
         executer.Execute("hogeMethod", parameterJson);
     }

     public void Callback()
     {
         Debug.Log("callback from js");
     }
 }