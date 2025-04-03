using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor.MovingPlatformRoute
{
    public interface IGoOnTurns
    {
        void SwitchTurn();
        void MakeAStep(int steps = 1);
    }
}

