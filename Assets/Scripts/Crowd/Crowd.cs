using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CrowdCounter))]
public class Crowd : MonoBehaviour
{
   public enum CrowdType
   {
      Player = 0,
      Enemy = 1
   }

   [SerializeField] private CrowdType type;

   [Header("Enemy Settings:")]
   [SerializeField] private int crowdSize;
   [SerializeField] private EnemyZone enemyZone;
   
   [Header("[DO NOT TOUCH]")]
   [SerializeField] private CrowdMember[] zeroCircle;
   [SerializeField] private CrowdMember[] firstCircle;
   [SerializeField] private CrowdMember[] secondCircle;
   [SerializeField] private CrowdMember[] thirdCircle;
   [SerializeField] private CrowdMember[] forthCircle;
   [SerializeField] private CrowdMember[] fifthCircle;
   [SerializeField] private CrowdMember[] sixthCircle;
   [SerializeField] private CrowdMember[] seventhCircle;
   [SerializeField] private CrowdMember[] eighthCircle;
   [SerializeField] private CrowdMember[] ninthCircle;
   
   public CrowdType MyCrowdType { get; private set; }
   
   private List<CrowdMember[]> _allCircles = new List<CrowdMember[]>();
   private bool _levelCompleted;

   private CrowdCounter _crowdCounter;

   private void Awake()
   {
      _crowdCounter = GetComponent<CrowdCounter>();
      FormListOfCircles();
      MyCrowdType = type;
      
      if (type == CrowdType.Player)
      {
         GameEvents.ChangeCrowdSize += ChangeCrowdSize;
         GameEvents.WinAnimation += OnWinAnimation;
         ChangeCrowdSize(1);
      }
      else
      {
         ChangeCrowdSize(crowdSize);
      }
   }
   
   private void OnDestroy()
   {
      if (type == CrowdType.Player)
      {
         GameEvents.ChangeCrowdSize -= ChangeCrowdSize;
         GameEvents.WinAnimation -= OnWinAnimation;
      }
   }

   public void KillCrowd()
   {
      gameObject.SetActive(false);
      if (enemyZone != null)
      {
         enemyZone.RemoveEnemyZone();
      }

      if (type == CrowdType.Player)
      {
         GameEvents.GameOver();
      }
   }
   
   private void FormListOfCircles()
   {
      _allCircles.Add(zeroCircle);
      _allCircles.Add(firstCircle);
      _allCircles.Add(secondCircle);
      _allCircles.Add(thirdCircle);
      _allCircles.Add(forthCircle);
      _allCircles.Add(fifthCircle);
      _allCircles.Add(sixthCircle);
      _allCircles.Add(seventhCircle);
      _allCircles.Add(eighthCircle);
      _allCircles.Add(ninthCircle);
   }
   
   private void ChangeCrowdSize(int amount)
   {
      if (amount < 0)
      {
         ShrinkCrowd(amount);
      }
      else
      {
         ExpandCrowd(amount);
      }
   }

   private void ShrinkCrowd(int amount)
   {
      var removedMembers = 0;
      var amountAbs = Mathf.Abs(amount);
      for (int i = _allCircles.Count-1; i >= 0; i--)
      {
         var circle = _allCircles[i];
         foreach (var crowdMember in circle)
         {
            var comp = crowdMember.gameObject;
            if (removedMembers == amountAbs) { break; }
            if (comp.activeSelf)
            {
               comp.SetActive(false);
               removedMembers++;                                         
            }
         }
      }
      _crowdCounter.UpdateMemberCounter(-removedMembers);
      if (_crowdCounter.EqualsZero() && !_levelCompleted)
      {
          KillCrowd();
      }
    }

   private void ExpandCrowd(int amount)
   {
      var addedMembers = 0;
      foreach (var circle in _allCircles)
      {
         foreach (var crowdMember in circle)
         {
            var comp = crowdMember.gameObject;
            if (addedMembers == amount) { break; }
            if (!comp.activeSelf)
            {
               comp.SetActive(true);
               addedMembers++;
            }
         }
      }
      _crowdCounter.UpdateMemberCounter(addedMembers);
   }

   private void OnWinAnimation()
   {
        _levelCompleted = true;
   }

}
