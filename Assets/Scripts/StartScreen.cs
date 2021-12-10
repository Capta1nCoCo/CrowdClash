using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartScreen : MonoBehaviour, IDragHandler
{
    private void Start()
    {
        Time.timeScale = 0;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

}
