using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.Gameplay
{
    public interface ITimeScaleObj
    {
        public float TimeScale { get; set; }
        public void NormalTime()
        {
            TimeScale = 1;
        }
        public void StopTime()
        {
            TimeScale = 0;
        }
    }
}