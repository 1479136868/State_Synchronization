using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventListener : EventTrigger
{
    /// <summary>
    /// 点击事件。
    /// </summary>
    public event Action<GameObject, BaseEventData> onClick;
    public event Action<GameObject, BaseEventData> onEnter;

    /// <summary>
    /// 挂载方法。
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public static UIEventListener Get(GameObject go)
    {
        UIEventListener ui = go.GetComponent<UIEventListener>();
        if (ui == null)
            ui = go.AddComponent<UIEventListener>();
        return ui;
    }


    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
    }

    public override void OnCancel(BaseEventData eventData)
    {
        base.OnCancel(eventData);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
    }

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
    }

    public override void OnInitializePotentialDrag(PointerEventData eventData)
    {
        base.OnInitializePotentialDrag(eventData);
    }

    public override void OnMove(AxisEventData eventData)
    {
        base.OnMove(eventData);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke(this.gameObject, eventData);
        base.OnPointerClick(eventData);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        this.onEnter?.Invoke(this.gameObject, eventData);
        base.OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
    }

    public override void OnScroll(PointerEventData eventData)
    {
        base.OnScroll(eventData);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        base.OnSubmit(eventData);
    }

    public override void OnUpdateSelected(BaseEventData eventData)
    {
        base.OnUpdateSelected(eventData);
    }
}

