using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTrigger : MonoBehaviour
{
    [SerializeField] private GameObject winFX;

    private void OnTriggerEnter(Collider other)
    {
        GameEvents.StopPlayerMovement(true);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        StartCoroutine(PlayWinFXAndFlyAway());              
    }

    private IEnumerator PlayWinFXAndFlyAway()
    {
        winFX.SetActive(true);

        yield return new WaitForSeconds(1.7f);
        winFX.SetActive(false);
        GameEvents.FlyAway(true);
    }
}
