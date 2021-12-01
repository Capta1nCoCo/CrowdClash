using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
	Vector3 offset;
    
	private void Start () 
	{
		offset = transform.position - player.position;
	}
    
	private void LateUpdate () 
	{
		Vector3 targetPos = player.position + offset;
		targetPos.x = 0;
		transform.position = targetPos;
	}
}