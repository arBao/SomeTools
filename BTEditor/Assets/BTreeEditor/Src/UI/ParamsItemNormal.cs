using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Reflection;


public class ParamsItemNormal : MonoBehaviour
{

    #region 属性
    public Text TextTitle;
    public InputField InputField;
    public PropertyInfo pInfo;
    public object infoObj;

    #endregion

    #region Start & Update
    void Start()
    {
        InputField.onValueChange.AddListener(OnValueChange);
    }

    void Update()
    {

    }
    #endregion



    #region Methods
    public void SetInputfieldType(InputField.ContentType type)
    {
        InputField.contentType = type;
    }

    void OnValueChange(string call)
    {
        //Debug.LogError("call  " + call);
        if (pInfo.PropertyType.Equals(typeof(System.String)))
        {
            if (call.Length == 0)
                call = "";
            pInfo.SetValue(infoObj, call,null);
        }
        else if (pInfo.PropertyType.Equals(typeof(System.Int32)) || pInfo.PropertyType.Equals(typeof(System.Int64)) || pInfo.PropertyType.Equals(typeof(System.Int16)))
        {
            if (call.Length == 0)
                call = "0";
            pInfo.SetValue(infoObj, int.Parse( call), null);
        }
        else if (pInfo.PropertyType.Equals(typeof(System.Single)) || pInfo.PropertyType.Equals(typeof(System.Double)))
        {
            if (call.Length == 0)
                call = "0";
            pInfo.SetValue(infoObj, float.Parse(call), null);
        }
    }

    #endregion


}