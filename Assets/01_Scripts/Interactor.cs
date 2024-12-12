using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

[RequireComponent(typeof(Rigidbody))]
public class Interactor : MonoBehaviour
{
    private enum Hands { Left, Right }
    [SerializeField] private Hands hand;
    [SerializeField] private List<InputActionAsset> m_ActionAssets;
    [SerializeField] private InputActionAsset ActionAssets;

    private Rigidbody rb;
    private IGrabAble currentItem;
    private GameObject currentPickedUpItem;
    private GameObject objectInterator;

    bool pickUpitem;


    private void Start()
    {
        SetActions();
        rb = GetComponent<Rigidbody>();
        objectInterator = new GameObject("ObjectInterator");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TestGrab();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            TestRelease();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            TestActivate();
        }

        if (pickUpitem == false) return;
        objectInterator.transform.position = transform.position;
        objectInterator.transform.rotation = transform.rotation;

    }


    private void Grab(InputAction.CallbackContext context)
    {
        bool objectInArea = CheckObjects();
        if (objectInArea == false) return;

        objectInterator.transform.position = currentItem.HoldPos.position;
        objectInterator.transform.rotation = currentItem.HoldPos.rotation; // een check
        currentPickedUpItem.transform.SetParent(objectInterator.transform, false);

        currentItem.HasBeenGrabed();
        pickUpitem = true;
    }

    private void TestGrab()
    {
        bool objectInArea = CheckObjects();
        if (objectInArea == false) return;

        objectInterator.transform.position = currentItem.HoldPos.position;
        objectInterator.transform.rotation = currentItem.HoldPos.rotation; // een check
        currentPickedUpItem.transform.SetParent(objectInterator.transform, false);

        currentItem.HasBeenGrabed();
        pickUpitem = true;

    }

    private void TestRelease()
    {
        if (currentItem == null) return;
        pickUpitem = false;
        objectInterator.transform.DetachChildren();

        if (currentPickedUpItem.TryGetComponent(out IPlaceAble placeAbleItem))
        {
            placeAbleItem.PlaceItem();
        }
        else
        {
            currentItem.HasBeenReleased();
        }

        currentItem = null;
        currentPickedUpItem = null;
    }

    private void TestActivate()
    {
        if (currentItem == null) return;
        if (currentPickedUpItem.TryGetComponent(out IActivateable activatableItem))
        {
            activatableItem.Activate();
        }
    }

    private void Release(InputAction.CallbackContext context)
    {
        if (currentItem == null) return;
        pickUpitem = false;
        objectInterator.transform.DetachChildren();

        if (currentPickedUpItem.TryGetComponent(out IPlaceAble placeAbleItem))
        {
            placeAbleItem.PlaceItem();
        }
        else
        {
            currentItem.HasBeenReleased();
        }

        currentItem = null;
        currentPickedUpItem = null;
    }

    private void Activate(InputAction.CallbackContext context)
    {
        if (currentItem == null) return;
        if (currentPickedUpItem.TryGetComponent(out IActivateable activatableItem))
        {
            activatableItem.Activate();
        }
    }

    private bool CheckObjects()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.TryGetComponent(out IGrabAble grabObject))
            {
                currentPickedUpItem = hitColliders[i].gameObject;
                currentItem = grabObject;
                return true;
            }
        }

        return false;
    }

    private void SetActions()
    {
        if (hand == Hands.Left)
        {
            var actionMapLeft = ActionAssets.FindActionMap("XRI LeftHand Interaction");
            var lefthandGrab = actionMapLeft.FindAction("Select");
            var lefthandActivate = actionMapLeft.FindAction("Activate");
            lefthandGrab.performed += Grab;
            lefthandGrab.canceled += Release;
            lefthandActivate.performed += Activate;

        }
        else
        {
            var actionMapRight = ActionAssets.FindActionMap("XRI RightHand Interaction");
            var righthandGrab = actionMapRight.FindAction("Select");
            var righthanActivate = actionMapRight.FindAction("Activate");
            righthandGrab.performed += Grab;
            righthandGrab.canceled += Release;
            righthanActivate.performed += Activate;
        }
    }
}
