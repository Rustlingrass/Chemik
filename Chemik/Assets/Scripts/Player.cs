using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IChemicalObjectParent {

    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedObjectChangedEventArgs> OnSelectedObjectChanged;
    public class OnSelectedObjectChangedEventArgs : EventArgs {
        public ChemicalObject selectedObject;
    }

    //Moving and looking
    [SerializeField] private Transform orientation;
    [SerializeField] private float playerSpeed = 2.0f;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;

    //Interaction
    [SerializeField] private Transform objectHolder;
    [SerializeField] private LayerMask interactingLayerMask;
    private RaycastHit raycastHit;
    private ChemicalObject selectedObject;
    private ChemicalObject chemicalObject;
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        GameInput.Instance.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;

        controller = gameObject.GetComponent<CharacterController>();

        cameraTransform = Camera.main.transform;
    }
    private void Update() {
        HandleMovement();
        HandleInteractionDetection();
    }
    private void GameInput_OnInteractAlternateAction(object sender, System.EventArgs e) {
        //InteractAlternate
        if (selectedObject != null) {
            if (chemicalObject != null && selectedObject.TryGetComponent<TubeHolder>(out TubeHolder tubeHolder)) {
                tubeHolder.InteractAlternateTubeHolder(chemicalObject);
            }
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (selectedObject != null) {
            //Has selectedObject
            if (chemicalObject == null) {
                //Is not holding anything
                selectedObject.SetChemicalObjectParent(this);
            }
        } else if (chemicalObject != null) {
            chemicalObject.SetChemicalObjectParent(null);
        }
        selectedObject = null;
    }
    private void HandleMovement() {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0) {
            playerVelocity.y = 0f;
        }

        Vector2 movement = GameInput.Instance.GetMoveInputVectorNormalized();
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;
        if (move != Vector3.zero) {
            orientation.forward = move;
        }
        controller.Move(move * Time.deltaTime * playerSpeed);
        transform.forward = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z);
    }
    private void HandleInteractionDetection() {
        SetSelectedObject(null);
        float rayLength = 2f;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out raycastHit, rayLength, interactingLayerMask)) {
            if (raycastHit.transform.TryGetComponent<ChemicalObject>(out ChemicalObject chemicalObject)) {
                if (selectedObject != chemicalObject) {
                    SetSelectedObject(chemicalObject);
                } else {
                    SetSelectedObject(null);
                }
            }
        }
    }

    public void SetSelectedObject(ChemicalObject chemicalObject) {
        selectedObject = chemicalObject;
        OnSelectedObjectChanged?.Invoke(this, new OnSelectedObjectChangedEventArgs {
            selectedObject = selectedObject
        });
    }

    public bool HasChemicalObject() {
        return chemicalObject != null;
    }

    public ChemicalObject GetChemicalObject() {
        return chemicalObject;
    }

    public Transform GetChemicalObjectFollowTransform() {
        return objectHolder;
    }

    public void SetChemicalObject(ChemicalObject chemicalObject) {
        this.chemicalObject = chemicalObject;

    }

    public void ClearChemicalObject() {
        chemicalObject = null;
    }
}
