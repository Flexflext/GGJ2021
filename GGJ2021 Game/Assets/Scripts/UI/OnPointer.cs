using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnPointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private List<Action<PointerEventData>> enterListeners = new List<Action<PointerEventData>>();
    private List<Action<PointerEventData>> exitListeners = new List<Action<PointerEventData>>();
        
    public void AddEnterListener(Action<PointerEventData> action)
    {
        enterListeners.Add(action);
    }
    
    public void AddExitListener(Action<PointerEventData> action)
    {
        exitListeners.Add(action);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (var listener in enterListeners)
        {
            listener.Invoke(eventData);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (var listener in exitListeners)
        {
            listener.Invoke(eventData);
        }
    }

}