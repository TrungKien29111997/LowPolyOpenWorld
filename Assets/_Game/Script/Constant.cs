using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constant
{
    public const string EVENT_NORMAL_TIME = "EVENT_NORMAL_TIME";
    public const string EVENT_STOP_TIME = "EVENT_STOP_TIME";
    public const string ANIM_SPEED = "MoveSpeed";
    public const string ANIM_JUMP = "Jump";
    public const string ANIM_ISGROUND = "IsGround";
    public const string ANIM_IDLE = "Idle";
    public static string EVENT_GAMEPLAY_UPDATE_SCORE = "EVENT_GAMEPLAY_UPDATE_SCORE";
    public const string TIMER_TICK_EVENT = "TIMER_TICK_EVENT";
    public const string EVENT_ON_NEW_DAY = "EVENT_ON_NEW_DAY";
    public const string SCENE_LOADING = "LoadingScene";
    public const string SCENE_MAIN_UI = "HomeScene";
    public const string SCENE_GAME_PLAY = "GameScene";
    public const string EVENT_CHEAT_MODE = "EVENT_CHEAT_MODE";
    public const string EVENT_CHANGE_PLAYER_RESOURCE = "EVENT_CHANGE_PLAYER_RESOURCE";

    public const string ADDRESSABLES_LABEL_COMMONRESOURCE_SO = "CommonResourceSO";
    public const string ADDRESSABLES_LABEL_WEAPON_SO = "WeaponSO";
    public const string ADDRESSABLES_LABEL_UI_PREFAB_SO = "UIPrefab";

    public const float RotationSmoothTime = 5f;
    public const float SpeedChangeRate = 10.0f;
    public const float Gravity = -10f;
    public const float JumpHeight = 1.2f;
    public const float TerminalVelocity = 70f;
}