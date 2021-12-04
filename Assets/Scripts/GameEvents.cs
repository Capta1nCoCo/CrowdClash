using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents
{
    public static Action<int> ChangeCrowdSize;

    public static Action<bool> StopPlayerMovement;

    public static Action GameOver;

    public static Action Victory;

    public static Action WinAnimation;

    public static Action FlyAway;
}
