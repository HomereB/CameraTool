using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFreeLookingState : CameraBaseState
{
    public override void CheckSwitchState()
    {
        if (Context.CurrentTarget != null)
        {
            SwitchState(Manager.GetState<CameraFreeMovementState>());
        }
    }

    public override void EnterState()
    {
        Debug.Log("nik");
    }

    public override void ExitState()
    {
        Debug.Log("toi");

    }

    public override void InitializeSubState(){}

    public override void UpdateState()
    {
        Context.RotateCamera(Context.RotationInput);
        CheckSwitchState();
    }
}
