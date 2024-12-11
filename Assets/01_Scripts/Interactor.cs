using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

[RequireComponent(typeof(Rigidbody))]
public class Interactor : MonoBehaviour
{
    [SerializeField] private List<InputActionAsset> m_ActionAssets;
    [SerializeField] private InputActionAsset ActionAssets;

    private Rigidbody rb;
    private GameObject holdingObject;
    private IGrabAble currentItem;
    private GameObject currentPickedUpItem;
    private GameObject ObjectInterator;

    bool pickUpitem;


    private void Start()
    {
        SetActions();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TestGrab();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {

        }

        if (pickUpitem == false) return;
        ObjectInterator.transform.position = transform.position;
    }


    private void Grab(InputAction.CallbackContext context)
    {
        CheckObjects();
    }

    private void TestGrab()
    {
        CheckObjects();
        ObjectInterator = new GameObject("test object");
        ObjectInterator.transform.position = currentItem.HoldPos.position;
        currentPickedUpItem.transform.SetParent(ObjectInterator.transform, false);

        currentItem.HasBeenGrabed();
        pickUpitem = true;

    }

    private void TestRelease()
    {

    }

    private void Release(InputAction.CallbackContext context)
    {

    }

    private void Activate(InputAction.CallbackContext context)
    {

    }

    private void CheckObjects()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.TryGetComponent(out IGrabAble grabObject))
            {
                currentPickedUpItem = hitColliders[i].gameObject;
                currentItem = grabObject;
                return;
                //grabObject.HasBeenGrabed(); return;
            }
        }




    }

    private void SetActions()
    {
        var actionMapLeft = ActionAssets.FindActionMap("XRI LeftHand Interaction");
        var actionMapRight = ActionAssets.FindActionMap("XRI RightHand Interaction");

        var lefthandGrab = actionMapLeft.FindAction("Select");
        var righthandGrab = actionMapRight.FindAction("Select");

        var lefthandActivate = actionMapLeft.FindAction("Activate");
        var righthanActivate = actionMapRight.FindAction("Activate");

        lefthandGrab.performed += Grab;
        righthandGrab.performed += Grab;

        lefthandGrab.canceled += Release;
        righthandGrab.canceled += Release;

        righthanActivate.performed += Activate;
        lefthandActivate.performed += Activate;
    }
}
