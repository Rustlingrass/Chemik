using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachinePOVExtension : CinemachineExtension {

    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float verticalSpeed = 10f;
    [SerializeField] private float clampAngle = 80f;
    [SerializeField] private Transform orientation;


    private GameInput gameInput;
    private Vector3 startingRotation;

    protected override void Awake() {
        base.Awake();
    }
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime) {
        if (vcam.Follow) {
            if (stage == CinemachineCore.Stage.Aim) {
                if (startingRotation == null) {
                    Vector2 deltaInput = GameInput.Instance.GetLookInputVectorNormalized();
                    startingRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;
                    startingRotation.y += deltaInput.y * horizontalSpeed * Time.deltaTime;
                    startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
                    state.RawOrientation = Quaternion.Euler(startingRotation.y, startingRotation.x, 0);
                }
            }
        }
    }
}