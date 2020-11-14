using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Reflection;

public class ParamsItemBool: MonoBehaviour
{

    #region 属性
    public Text TextTitle;
    public ButtonCustom BtnBoolVlaue;
    public Image ImageGou;

    public bool isSelected = false;

    public PropertyInfo pInfo;
    public object infoObj;
    #endregion

    #region Start & Update
    void Start () {
        BtnBoolVlaue.onClickCustom = OnClickBtnBoolVlaue;
        SetIsSelect(isSelected);
	}
	
	void Update () {

    }
    #endregion


    #region OnClicks

    void OnClickBtnBoolVlaue(GameObject sender)
    {
        isSelected = !isSelected;
        SetIsSelect(isSelected);

        pInfo.SetValue(infoObj, isSelected, null);
    }

    #endregion

    #region Methods

    public void SetIsSelect(bool select)
    {
        isSelected = select;
        ImageGou.gameObject.SetActive(isSelected);
    }

    #endregion


}