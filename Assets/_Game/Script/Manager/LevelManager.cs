using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
namespace Core.Gameplay
{
    public class LevelManager : Singleton<LevelManager>
    {
        [field: SerializeField] public MainCameraControl CamCtrl { get; private set; }
        [field: SerializeField] public CinemachineVirtualCamera CamFollowPlayer { get; private set; }
    }
}