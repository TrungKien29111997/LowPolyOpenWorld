using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.Gameplay
{
    public class MainCameraControl : MonoBehaviour
    {
        Transform tf;
        public Transform TF { get { return tf ??= transform; } }
        [field: SerializeField] public Camera MainCamera { get; private set; }
        [SerializeField] CinemachineBrain cameraBrain;
        public void ChangeBlend(CinemachineBlendDefinition.Style tmpStyle, float blendTime)
        {
            cameraBrain.m_DefaultBlend.m_Style = tmpStyle;
            if (tmpStyle != CinemachineBlendDefinition.Style.Cut)
            {
                cameraBrain.m_DefaultBlend.m_Time = blendTime;
            }
        }
    }
}