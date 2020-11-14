using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Button扩展
public class ButtonCustom : Button
{

    public enum ButtonState
    {
        Normal,
        Selected,
        Diasbled,
    }

    public delegate void VoidDelegateObj(GameObject go);
    public VoidDelegateObj onClickCustom;
    public VoidDelegateObj onPointerEnter;
    public VoidDelegateObj onPointerExit;
    public Image imageNormalState;
    public Image imageSelectedState;
    public Image imageDisableState;

    public Image imageTextNormalState;
    public Image imageTextSelectedState;
    public Image imageTextDisableState;

    public Text textButton;
    public Color textColorNormalState;
    public Color textColorSelectedState;
    public Color textColorDisableState;

    public Image hideImage;
    public bool hideNormalState;
    public bool hideSelectedState;
    public bool hideDisableState;

    public override void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if (onPointerEnter != null)
            onPointerEnter(gameObject);
    }

    public override void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        if (onPointerExit != null)
            onPointerExit(gameObject);
    }

    public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (onClickCustom != null)
            onClickCustom(gameObject);
    }

    public void SetDisableColor(bool disable)
    {
        if (disable)
        {
            GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
        }
        else
        {
            GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public void SetState(ButtonState state)
    {
        switch (state)
        {
            case ButtonState.Normal:
                if (imageNormalState != null)
                    imageNormalState.gameObject.SetActive(true);
                if (imageSelectedState != null)
                    imageSelectedState.gameObject.SetActive(false);
                if (imageDisableState != null)
                    imageDisableState.gameObject.SetActive(false);
                if (imageTextNormalState != null)
                    imageTextNormalState.gameObject.SetActive(true);
                if (imageTextSelectedState != null)
                    imageTextSelectedState.gameObject.SetActive(false);
                if (imageTextDisableState != null)
                    imageTextDisableState.gameObject.SetActive(false);
                if (textButton != null)
                {
                    textButton.color = textColorNormalState;
                }
                if (hideImage != null)
                {
                    if (hideNormalState)
                    {
                        hideImage.gameObject.SetActive(false);
                    }
                    else
                    {
                        hideImage.gameObject.SetActive(true);
                    }
                }
                break;
            case ButtonState.Selected:
                if (imageNormalState != null)
                    imageNormalState.gameObject.SetActive(false);
                if (imageSelectedState != null)
                    imageSelectedState.gameObject.SetActive(true);
                if (imageDisableState != null)
                    imageDisableState.gameObject.SetActive(false);

                if (imageTextNormalState != null)
                    imageTextNormalState.gameObject.SetActive(false);
                if (imageTextSelectedState != null)
                    imageTextSelectedState.gameObject.SetActive(true);
                if (imageTextDisableState != null)
                    imageTextDisableState.gameObject.SetActive(false);
                if (textButton != null)
                {
                    textButton.color = textColorSelectedState;
                }
                if (hideImage != null)
                {
                    if (hideSelectedState)
                    {
                        hideImage.gameObject.SetActive(false);
                    }
                    else
                    {
                        hideImage.gameObject.SetActive(true);
                    }
                }
                break;
            case ButtonState.Diasbled:
                if (imageNormalState != null)
                    imageNormalState.gameObject.SetActive(false);
                if (imageSelectedState != null)
                    imageSelectedState.gameObject.SetActive(false);
                if (imageDisableState != null)
                    imageDisableState.gameObject.SetActive(true);

                if (imageTextNormalState != null)
                    imageTextNormalState.gameObject.SetActive(false);
                if (imageTextSelectedState != null)
                    imageTextSelectedState.gameObject.SetActive(false);
                if (imageTextDisableState != null)
                    imageTextDisableState.gameObject.SetActive(true);
                if (textButton != null)
                {
                    textButton.color = textColorDisableState;
                }
                if (hideImage != null)
                {
                    if (hideDisableState)
                    {
                        hideImage.gameObject.SetActive(false);
                    }
                    else
                    {
                        hideImage.gameObject.SetActive(true);
                    }
                }
                break;
        }
    }

    public void SetImagesForButtonState(Image normalState, Image selectedState)
    {
        SetImagesForButtonState(normalState, selectedState, null);
    }

    public void SetImagesForButtonState(Image normalState, Image selectedState, Image disableState)
    {
        imageNormalState = normalState;
        imageSelectedState = selectedState;
        imageDisableState = disableState;
    }

    public void SetImageTextsForButtonState(Image normalState, Image selectedState)
    {
        SetImageTextsForButtonState(normalState, selectedState, null);
    }

    public void SetImageTextsForButtonState(Image normalState, Image selectedState, Image disableState)
    {
        imageTextNormalState = normalState;
        imageTextSelectedState = selectedState;
        imageTextDisableState = disableState;
    }


    public void SetTextColorForButtonState(Text text, Color normalState, Color selectedState, Color disableState)
    {
        textButton = text;
        textColorNormalState = normalState;
        textColorSelectedState = selectedState;
        textColorDisableState = disableState;
    }

    public void SetImageShouldHideForState(Image image, bool normalState, bool selectedState, bool disableState)
    {
        hideImage = image;
        hideNormalState = normalState;
        hideSelectedState = selectedState;
        hideDisableState = disableState;
    }

}
