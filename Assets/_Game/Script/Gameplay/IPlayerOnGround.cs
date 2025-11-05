using DG.Tweening;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;
namespace Core.Gameplay
{
    public class IPlayerOnGround : IPlayerBehavious
    {
        float verticalVelocity;
        bool isGround;
        bool isInAir;
        float jumpTimeOut;
        Player player;
        float speedMovement;
        float animationBlend;
        Vector3 directMove;
        InputGamePlay input => InputGamePlay.Instance;
        public Vector3 Forward2D
        {
            get
            {
                if (this.player == null)
                {
                    return Vector3.zero;
                }
                else
                {
                    return Ex.Extension.ProjectOntoPlane(LevelManager.Instance.CamCtrl.TF.forward, this.player.DirectGravity);
                }
            }
        }
        public Vector3 Right2D
        {
            get
            {
                if (this.player == null)
                {
                    return Vector3.zero;
                }
                else
                {
                    return Ex.Extension.ProjectOntoPlane(LevelManager.Instance.CamCtrl.TF.right, this.player.DirectGravity);
                }
            }
        }
        public void OnEnter(Player player)
        {
            this.player = player;
            player.cameraThirdViewControl.lockCamera = false;
            player.Col.enabled = true;
            player.Rb.isKinematic = false;
        }

        public void OnUpdate(Player player)
        {
            Move(player);
            // isGround = player.GroundedCheck();
            // player.anim.SetBool(Constant.ANIM_ISGROUND, isGround);
            JumpAndGravity(player);
        }
        void Move(Player player)
        {
            if (input.Move.sqrMagnitude < 0.1f)
            {
                speedMovement = 0f;
            }
            else if (input.Move.sqrMagnitude > 0.1f)
            {
                speedMovement = 50f * (input.Sprint ? player.SprintSpeed : player.MoveSpeed);
            }
            animationBlend = Mathf.Lerp(animationBlend, speedMovement * 0.02f, player.DeltaTime * Constant.SpeedChangeRate);
            if (animationBlend < 0.01f) animationBlend = 0f;
            player.anim.SetFloat(Constant.ANIM_SPEED, animationBlend);
            directMove = (Forward2D * input.Move.y + Right2D * input.Move.x).normalized;
        }
        // void ChangeTimeScale()
        // {
        //     float scaleValue = this.player.IsUnscaleTime ? 1f : GamePlayManager.Instance.ScaleTime;
        //     verticalVelocity *= scaleValue;
        //     arrTween.ForEach(tween =>
        //     {
        //         tween.DotWeenTimeScale(scaleValue);
        //     });
        // }

        public void OnFixUpdate(Player player)
        {
            if (input.Move.sqrMagnitude > 0.1f)
            {
                player.TF.rotation = Quaternion.RotateTowards(player.TF.rotation, Quaternion.LookRotation(directMove, -player.DirectGravity), Constant.RotationSmoothTime);
            }
            player.Rb.velocity = player.TF.forward * speedMovement * player.FixDeltaTime; // - player.DirectGravity * verticalVelocity;
        }

        public void OnExit(Player player)
        {

        }
        void JumpAndGravity(Player player, System.Action afterFallAction = null)
        {
            if (isGround)
            {
                if (jumpTimeOut > 0.3f)
                {
                    isInAir = false;
                    jumpTimeOut = 0;
                    afterFallAction?.Invoke();
                }
                if (verticalVelocity < 0.1f)
                {
                    verticalVelocity = -0.2f * player.TimeScale;
                }

                if (input.Jump && jumpTimeOut < 0.1f)
                {
                    isInAir = true;
                    player.ChangeAnim(Constant.ANIM_JUMP);
                    // v = can bac hai(-2f * g * jumpHeight) cong thuc tinh gia toc trong truong
                    verticalVelocity = Mathf.Sqrt(Constant.JumpHeight * -2f * Constant.Gravity * Mathf.Pow(player.TimeScale, 2));
                }
            }
            if (isInAir)
            {
                jumpTimeOut += player.DeltaTime;
            }
            if (verticalVelocity < Constant.TerminalVelocity * player.TimeScale)
            {
                verticalVelocity += Constant.Gravity * player.DeltaTime;
            }
            else
            {
                verticalVelocity = Constant.TerminalVelocity * player.TimeScale;
            }
        }
    }
}