using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.RemoteConfig;

public class ChangeCubeColor : MonoBehaviour
{

    public struct userAttributes { 
    
    }
    public struct appAttributes { 
    }

    public bool isBlue = false;

    public Material blue;

    public Material red;

    public MeshRenderer _mRender;

    // Start is called before the first frame update
    void Awake()
    {

        ConfigManager.FetchCompleted += SetColor;
        ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());



    }
     
    void SetColor (ConfigResponse response)
    {
       
        isBlue = ConfigManager.appConfig.GetBool("CubeIsBlue");
        Debug.LogFormat("Remote Config Fetch over, isBlue:{0}", isBlue);
        if (isBlue)
        {
            _mRender.material = blue;
        }
        else
        {
            _mRender.material = red;
        }
    } 


    // Update is called once per frame
    void Update()
    {
      if(Input.GetMouseButtonDown(0))
        {
            ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
        }
    }

    private void OnDestroy()
    {
        ConfigManager.FetchCompleted -= SetColor;
    }
}
