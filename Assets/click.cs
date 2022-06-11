using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Vuforia;

public class click : MonoBehaviour
{
    InputField Phvalue;
    InputField Temp;
    public VirtualButtonBehaviour Vb_on;
 
    void Start()
    {
        Phvalue = GameObject.Find("InputFieldPH").GetComponent<InputField>();
        
        Temp = GameObject.Find("InputFieldTemp").GetComponent<InputField>();

        Vb_on.RegisterOnButtonPressed(OnButtonPressed_on);
        // GameObject.Find("GetButton").GetComponent<Button>().onClick.AddListener(GetData);
    }

    public void OnButtonPressed_on(VirtualButtonBehaviour Vb_on)
    {
        GetData_tem();
        GetData_ph();
        Debug.Log("Click");
    }
 
    void GetData_tem() => StartCoroutine(GetData_Coroutine());
    void GetData_ph() => StartCoroutine(GetData_Coroutine1());
 
    IEnumerator GetData_Coroutine1()
    {
        Debug.Log("Getting Data");
        Phvalue.text = "Loading...";
        string uri = "http://blynk-cloud.com/lkRu9-RDqlyIypcDDsDv5_h606nCriol/get/v2";
        using(UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();
            if ((request.result == UnityWebRequest.Result.ConnectionError) || (request.result == UnityWebRequest.Result.ProtocolError))
                Phvalue.text = request.error;
            else
            {
                Phvalue.text = request.downloadHandler.text;
                Phvalue.text = Phvalue.text.Substring(2,5);
            }
        }
    }
    IEnumerator GetData_Coroutine()
    {
        Debug.Log("Getting Data");
        Temp.text = "Loading...";
        string uri = "http://blynk-cloud.com/lkRu9-RDqlyIypcDDsDv5_h606nCriol/get/v0";
        using(UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();
            if ((request.result == UnityWebRequest.Result.ConnectionError) || (request.result == UnityWebRequest.Result.ProtocolError))
                Temp.text = request.error;
            else
            {
                Temp.text = request.downloadHandler.text;
                Temp.text = Temp.text.Substring(2,5);
            }
        }
    }
}