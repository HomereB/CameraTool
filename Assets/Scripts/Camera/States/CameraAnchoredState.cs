using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnchoredState : CameraBaseState
{
    public CameraAnchoredState() : base()
    {
        IsRootState = true;
    }

    public CameraAnchoredState(CameraController currentContext, CameraStateManager currentManager) : base(currentContext, currentManager)
    {
        IsRootState = true;
    }
    public override void CheckSwitchState()
    {
        if(Context.IsTraveling)
        {
            Debug.Log("yo");

            SwitchState(Manager.GetState<CameraCutsceneState>());
        }
        if (Context.CurrentAnchor == null)
        {
            SwitchState(Manager.GetState<CameraFreeMovementState>());
        }
    }

    public override void EnterState()
    {
        InitializeSubState();
    }

    public override void ExitState()
    {

    }

    public override void InitializeSubState()
    {
        if (Context.CurrentTarget != null)
        {
            SetSubState(Manager.GetState<CameraTargetingState>());
        }
        else
        {
            SetSubState(Manager.GetState<CameraFreeLookingState>());
        }
    }

    public override void UpdateState()
    {
        Context.CurrentAnchor.MoveCameraAnchor(Context.MovementInput);
        Context.FollowAnchor();
        CheckSwitchState();
    }
}
