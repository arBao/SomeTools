using UnityEngine;
using System.Collections;

public class NodeUIBase: MonoBehaviour
{

    #region 属性

    public ButtonCustom Button;
    public GameAI.BNode node;

    #endregion

    #region Start & Update
    void Start () {
	    Button.onClickCustom = OnClickButton;
	}
	
	void Update () {

    }
    #endregion


    #region OnClicks

    public virtual void OnClickButton(GameObject sender)
    {

    }

    #endregion

    #region Methods

    #endregion


}