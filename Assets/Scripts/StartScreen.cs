using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartScreen : MonoBehaviour
{
    private void Start()
    {
        GameEvents.StopPlayerMovement(true);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                gameObject.SetActive(false);
                GameEvents.StopPlayerMovement(false);
            }
        }
    }
}
