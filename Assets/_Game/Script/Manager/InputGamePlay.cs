using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.Gameplay
{
    public class InputGamePlay : Singleton<InputGamePlay>
    {
        public Vector2 Move => playerInput.Movement.Move.ReadValue<Vector2>();
        public bool Sprint => playerInput.Movement.Sprint.IsPressed();
        public Vector2 Look => playerInput.Movement.Look.ReadValue<Vector2>();
        public float Zoom => playerInput.Movement.Zoom.ReadValue<float>();
        public bool Attack => playerInput.Movement.Attack.triggered;
        //public bool Cutoff => playerInput.UIGameplay.Cutoff.triggered;
        public bool Jump => playerInput.Movement.Jump.triggered;
        //public bool ChangeGravity => playerInput.Movement.ChangeGravity.triggered;
        //public bool Player1 => playerInput.Movement.Player1.triggered;
        //public bool Player2 => playerInput.Movement.Player2.triggered;

        InputCtrl playerInput;
        protected override void OnAwake()
        {
            base.OnAwake();
            playerInput = new InputCtrl();
        }
        private void OnEnable()
        {
            playerInput.Enable();
        }

        private void OnDisable()
        {
            playerInput.Disable();
        }


        //Dive = playerInput.Movement.Dive.triggered;
        //Aim = playerInput.Movement.Aim.triggered;
        //Parry = playerInput.Movement.Parry.IsPressed();
        //ESkill = playerInput.Movement.ESkill.triggered;
        //QSkill = playerInput.Movement.QSkill.triggered;
        //TSkill = playerInput.Movement.TSkill.triggered;
        //Collect = playerInput.Movement.Collect.triggered;
        //WaitingHall = playerInput.GamePlayUI.WaitingHall.triggered;
        //MouseVisible = playerInput.GamePlayUI.MouseVisible.IsPressed();

    }
}