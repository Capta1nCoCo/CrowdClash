using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegroupTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameEvents.ReGroupCrowd();
        gameObject.SetActive(false);
    }
}
