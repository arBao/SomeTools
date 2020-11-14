using UnityEngine;
using System.Collections;

//事件监听器
public class EventTriggerListener : UnityEngine.EventSystems.EventTrigger 
{
    public delegate void VoidDelegateGameObject(GameObject go);
    public delegate void VoidDelegateData(UnityEngine.EventSystems.PointerEventData eventData);
    public VoidDelegateGameObject onClick;
    public VoidDelegateGameObject onDown;
    public VoidDelegateGameObject onEnter;
    public VoidDelegateGameObject onExit;
    public VoidDelegateGameObject onUp;
    public VoidDelegateGameObject onSelect;
    public VoidDelegateGameObject onUpdateSelect;
    public VoidDelegateData onDrag;

    static public EventTriggerListener Get(GameObject go)
    {
        EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
        if (listener == null) listener = go.AddComponent<EventTriggerListener>();
        return listener;
    }

    public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (onClick != null) onClick(gameObject);
    }

    public override void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (onDrag != null) onDrag(eventData);
    }

    public override void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (onDown != null) onDown(gameObject);
    }

    public override void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (onExit != null) onExit(gameObject);
    }

    public override void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (onUp != null) onUp(gameObject);
    }

    public override void OnSelect(UnityEngine.EventSystems.BaseEventData eventData)
    {
        if (onSelect != null) onSelect(gameObject);
    }

    public override void OnUpdateSelected(UnityEngine.EventSystems.BaseEventData eventData)
    {
        if (onUpdateSelect != null) onUpdateSelect(gameObject);
    }
}
