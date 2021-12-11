using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(CrowdCounter))]
public class WinAnimation : MonoBehaviour
{
    [SerializeField] private GameObject crowdMemberPrefab;
    [SerializeField] private Transform centralMember;
    [SerializeField] private Transform pyramidParent;
    
    private int _memberAmount;    
    private float verticalOffset = 0.24f;
    private float horizontalOffset = 0.08f;

    private CrowdCounter _crowdCounter;
    private PlayerMovement _playerMovement;
    private CameraFollow _cameraFollow;

    private void Awake()
    {
        _crowdCounter = GetComponent<CrowdCounter>();
        _playerMovement = GetComponent<PlayerMovement>();
        _cameraFollow = FindObjectOfType<CameraFollow>();

        GameEvents.WinAnimation += StartWinAnimation;
    }

    private void OnDestroy()
    {
        GameEvents.WinAnimation -= StartWinAnimation;
    }

    private void StartWinAnimation()
    {
        GameEvents.StopPlayerMovement(true);
        _playerMovement.StopPlayersControl();
        _memberAmount = _crowdCounter.GetMemberCounter();
        AddMemberAmountToScore();
        _crowdCounter.RemoveCounterText();
        _cameraFollow.ChangeCameraAngle();

        if (_crowdCounter.GetMemberCounter() == 1)
        {
            GameEvents.StopPlayerMovement(false);
        }
        else
        {
            StartCoroutine(BuildCrowdPyramid());
        }               
    }

    private void AddMemberAmountToScore()
    {
        var currentScore = PlayerPrefs.GetInt("score");
        PlayerPrefs.SetInt("score", currentScore + _memberAmount);
    }

    private IEnumerator BuildCrowdPyramid()
    {
        Vector3 centralPos = centralMember.position;
        int rowSize = 1;
        float rowOffset = 0;

        for (var i = 0; i < _memberAmount; i++)
        {            
            int currentRow = 0;
            GameObject instance = centralMember.gameObject;
            while (currentRow < rowSize)
            {
                if (currentRow > 0)
                {
                    _memberAmount--;
                }
                var prevInstancePos = instance.transform.position;
                Vector3 newPos = currentRow == 0 ? new Vector3(centralPos.x - rowOffset, centralPos.y, centralPos.z) : new Vector3(prevInstancePos.x + horizontalOffset, centralPos.y, centralPos.z);
                
                GameEvents.ChangeCrowdSize(-1);
                instance = Instantiate(crowdMemberPrefab, newPos, Quaternion.identity, pyramidParent);
                currentRow++;
            }            
            
            pyramidParent.position = new Vector3(pyramidParent.position.x, pyramidParent.position.y + verticalOffset, pyramidParent.position.z);
            rowSize++;
            rowOffset += horizontalOffset / 2;                       
            yield return new WaitForSeconds(.1f);
        }

        pyramidParent.position = new Vector3(pyramidParent.position.x, pyramidParent.position.y - verticalOffset, pyramidParent.position.z);
        GameEvents.StopPlayerMovement(false);
    }   
}
