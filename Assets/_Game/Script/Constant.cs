using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core
{
    public static class Constant
    {
        public const string EVENT_NORMAL_TIME = "EVENT_NORMAL_TIME";
        public const string EVENT_STOP_TIME = "EVENT_STOP_TIME";
        public const string ANIM_SPEED = "MoveSpeed";
        public const string ANIM_JUMP = "Jump";
        public const string ANIM_ISGROUND = "IsGround";
        public const string ANIM_IDLE = "Idle";

        public const float RotationSmoothTime = 5f;
        public const float SpeedChangeRate = 10.0f;
        public const float Gravity = -10f;
        public const float JumpHeight = 1.2f;
        public const float TerminalVelocity = 70f;
    }
}