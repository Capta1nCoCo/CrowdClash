using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float jumpDuration = 1f;

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<BoxCollider>().enabled = false;
        StartCoroutine(Jump());
    }

    private IEnumerator Jump()
    {
        GameEvents.FlyAway(true);

        yield return new WaitForSeconds(jumpDuration);

        GameEvents.FlyAway(false);
    }
}
