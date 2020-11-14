using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Reflection;
using System;

public class ParamsItemEnum : MonoBehaviour
{

    #region 属性
    public Text TextTitle;
    public ButtonCustom BtnEnumVlaue;
    public Transform EnumItemsContainer;
    public PropertyInfo pInfo;
    public object infoObj;

    bool showContainer = false;
    #endregion

    #region Start & Update
    void Start()
    {
        BtnEnumVlaue.onClickCustom = OnClickBtnEnumVlaue;
    }
    #endregion


    #region OnClicks

    void OnClickBtnEnumVlaue(GameObject sender)
    {
        ShowEnumsContainer(!showContainer);
    }

    void OnClickEnumItem(GameObject sender)
    {
        Debug.LogError("sender.name  " + sender.name);
        ShowEnumsContainer(false);
        FieldInfo fiSelect = null;
        FieldInfo[] fields = pInfo.PropertyType.GetFields(BindingFlags.Static | BindingFlags.Public);
        for (int i = 0; i < fields.Length; i++)
        {
            FieldInfo fi = fields[i];
            if(fi.Name.Equals(sender.name))
            {
                fiSelect = fi;
            }
        }

        pInfo.SetValue(infoObj, fiSelect.GetValue(null), null);
        SetIsSelect((System.Enum)fiSelect.GetValue(null));
    }

    #endregion

    #region Methods

    void ShowEnumsContainer(bool show)
    {
        showContainer = show;

        EnumItemsContainer.gameObject.SetActive(showContainer);
    }

    public void SetIsSelect(System.Enum select)
    {
        Debug.LogError("-------------------------------select Enum " + select);
        BtnEnumVlaue.transform.Find("Text").GetComponent<Text>().text = select.ToString();

    }

    public void ResetEnumsWithType(Type type)
    {
        FieldInfo[] fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);

        Vector2 size = EnumItemsContainer.GetComponent<RectTransform>().sizeDelta;
        size.y = fields.Length * 40;
        EnumItemsContainer.GetComponent<RectTransform>().sizeDelta = size;

        for (int i = 0; i < fields.Length; i++)
        {
            FieldInfo fi = fields[i];
            GameObject obj = GameObject.Instantiate(Resources.Load("Prefab/EnumItem")) as GameObject;
            obj.transform.SetParent(EnumItemsContainer);
            obj.GetComponent<RectTransform>().localScale = Vector3.one;
            Vector3 pos = new Vector3(0, 20 + i * 40, 0);
            obj.GetComponent<RectTransform>().localPosition = pos;
            obj.transform.Find("Text").GetComponent<Text>().text = fi.Name;
            obj.GetComponent<ButtonCustom>().onClickCustom = OnClickEnumItem;
            obj.name = fi.Name;
        }

    }

    #endregion


}