using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
	private Vector3 _offset;
	private float _xCameraPos;

    private void Awake()
    {
		_xCameraPos = transform.position.x;
    }

    private void Start () 
	{
		_offset = transform.position - player.position;				
	}
    
	private void LateUpdate () 
	{
		Vector3 targetPos = player.position + _offset;
		targetPos.x = _xCameraPos + player.position.x / 3;
		transform.position = targetPos;		
	}

	public void ChangeCameraAngle()
    {
		_xCameraPos = 3f;
		_offset = new Vector3(_offset.x, _offset.y, _offset.z - 2.6f);
		transform.Rotate(new Vector3(transform.rotation.x - 17.5f, transform.rotation.y - 20f, transform.rotation.z + 15f));
	}
}
