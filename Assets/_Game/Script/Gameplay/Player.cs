using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ex;
using Sirenix.OdinInspector;

namespace Core.Gameplay
{
    public class Player : PoolingElement, ITimeScaleObj
    {
        ITimeScaleObj timeScaleObj => this;
        public float TimeScale => timeScaleObj.TimeScale;
        public float DeltaTime => timeScaleObj.TimeScale * Time.deltaTime;
        public float FixDeltaTime => timeScaleObj.TimeScale * Time.fixedDeltaTime;
        [field: SerializeField] public CapsuleCollider Col { get; private set; }
        [field: SerializeField] public Rigidbody Rb { get; private set; }
        public float SprintSpeed, MoveSpeed;
        public Animator anim;
        public Vector3 DirectGravity => Vector3.down;
        float ITimeScaleObj.TimeScale { get; set; }
        public CameraThirdViewControl cameraThirdViewControl;
        string currentAnim;
        IPlayerBehavious currentBehavious;
        public void Init()
        {
            Rb.isKinematic = true;
            Rb.useGravity = false;
            Col.enabled = false;
            // rayCastG.gameObject.SetActive(false);
            // cameraThirdViewControl.enabled = false;
            // aiPath.enabled = false;
            // aiDestinationSetter.enabled = false;
        }
        void Start()
        {
            timeScaleObj.TimeScale = 1;
            ChangeState(new IPlayerOnGround());
        }
        void Update()
        {
            currentBehavious?.OnUpdate(this);
        }
        void FixedUpdate()
        {
            currentBehavious?.OnFixUpdate(this);
        }
        public void ChangeState(IPlayerBehavious behavious)
        {
            currentBehavious?.OnExit(this);
            currentBehavious = behavious;
            Init();
            currentBehavious?.OnEnter(this);
        }
        public void ChangeAnim(string animName)
        {
            anim.ChangeAnim(ref currentAnim, animName);
        }
#if UNITY_EDITOR
        [Button]
        void Editor()
        {
            Col = GetComponent<CapsuleCollider>();
            Rb = GetComponent<Rigidbody>();
        }
#endif
    }
}