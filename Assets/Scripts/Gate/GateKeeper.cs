using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKeeper : MonoBehaviour
{
    [SerializeField] private Gateway gatewayLeft;
    [SerializeField] private Gateway gatewayRight;

    public void DeactivateGates()
    {
        gatewayLeft.gameObject.GetComponent<MeshCollider>().enabled = false;
        gatewayRight.gameObject.GetComponent<MeshCollider>().enabled = false;
    }
}
