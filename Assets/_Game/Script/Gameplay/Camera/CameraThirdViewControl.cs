using Cinemachine;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows;
namespace Core.Gameplay
{
    public class CameraThirdViewControl : MonoBehaviour
    {
        Transform tf;
        public Transform TF { get { return tf ??= transform; } }
        InputGamePlay inputCtrl => InputGamePlay.Instance;
        CinemachineVirtualCamera virtualCamera => LevelManager.Instance.CamFollowPlayer;
        [SerializeField] Transform cameraRot;

        [SerializeField]
        float
            topClamp = 180f,
            bottomClamp = -180f,
            topFieldView = 100f,
            bottomFieldView = 20f,
            CameraAngleOverride = 0.0f;
        //[SerializeField] bool LockCameraPosition = false;
        float cinemachineTargetYaw;
        float cinemachineTargetPitch;
        private const float threshold = 0.01f;
        public bool lockCamera { get; set; }
        [SerializeField] Transform targetFollow;
        private void Awake()
        {
            // TigerForge.EventManager.StartListening(Constant.EVENT_MODE_CUT_OFF, () =>
            // {
            //     LockCameraPosition = GamePlayManager.Instance.IsCutoffMode;
            // });
            // TigerForge.EventManager.StartListening(Constant.EVENT_CHANGE_GRAVITY, Lock);
            // TigerForge.EventManager.StartListening(Constant.EVENT_CHANGE_GRAVITY_DONE, UnLock);
        }
        // void Lock()
        // {
        //     LockCameraPosition = true;
        // }
        // void UnLock()
        // {
        //     LockCameraPosition = false;
        // }

        private void LateUpdate()
        {
            if (targetFollow != null)
            {
                TF.position = targetFollow.position;
                TF.up = targetFollow.up;
                if (lockCamera) return;
                CameraRotation();
            }
        }
        void CameraRotation()
        {
            // if there is an input and camera position is not fixed
            if (inputCtrl.Look.sqrMagnitude >= threshold)
            {
                cinemachineTargetYaw += inputCtrl.Look.x;
                cinemachineTargetPitch += inputCtrl.Look.y;
            }

            // clamp our rotations so our values are limited 360 degrees
            cinemachineTargetYaw = ClampAngle(cinemachineTargetYaw, float.MinValue, float.MaxValue);
            cinemachineTargetPitch = ClampAngle(cinemachineTargetPitch, bottomClamp, topClamp);

            // Cinemachine will follow this target
            cameraRot.localRotation = Quaternion.Euler(cinemachineTargetPitch + CameraAngleOverride, cinemachineTargetYaw, 0.0f);

            // zoom player
            if (inputCtrl.Zoom > 0 && virtualCamera.m_Lens.FieldOfView < topFieldView)
            {
                virtualCamera.m_Lens.FieldOfView += 1;
            }
            if (inputCtrl.Zoom < 0 && virtualCamera.m_Lens.FieldOfView > bottomFieldView)
            {
                virtualCamera.m_Lens.FieldOfView -= 1;
            }
        }

        float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}