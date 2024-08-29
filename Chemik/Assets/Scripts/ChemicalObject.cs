using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalObject : MonoBehaviour
{
    [SerializeField] private ChemicalObjectSO chemicalObjectSO;
    private IChemicalObjectParent chemicalObjectParent;
    private Rigidbody rb;
    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }
    public virtual void SetChemicalObjectParent(IChemicalObjectParent chemicalObjectParent) {
        if (this.chemicalObjectParent != null) {
            this.chemicalObjectParent.ClearChemicalObject();
        }
        if (chemicalObjectParent == null) {
            transform.parent = null;
            rb.isKinematic = false;
        } else {
            this.chemicalObjectParent = chemicalObjectParent;
            if (chemicalObjectParent.HasChemicalObject()) {
                Debug.LogError(chemicalObjectParent + "already has a chemicalObject");
            }
            chemicalObjectParent.SetChemicalObject(this);

            rb.isKinematic = true;
            transform.parent = chemicalObjectParent.GetChemicalObjectFollowTransform();
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    public IChemicalObjectParent GetChemicalObjectParent() {
        return chemicalObjectParent;
    }
    public void DestroySelf() {
        chemicalObjectParent.ClearChemicalObject();
        Destroy(gameObject);
    }

    public ChemicalObjectSO GetChemicalObjectSO() {
        return chemicalObjectSO;
    }
}
