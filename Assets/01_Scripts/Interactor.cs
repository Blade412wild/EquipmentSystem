using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

[RequireComponent(typeof(Rigidbody))]
public class Interactor : MonoBehaviour
{
    public event Action<Interactor> OnActivatedObject;
    private enum Hands { Left, Right }
    public GameObject currentPickedUpItem { get; private set; }
    public Rigidbody Rb {get; private set;}

    [SerializeField] private Hands hand;
    [SerializeField] private List<InputActionAsset> m_ActionAssets;
    [SerializeField] private InputActionAsset ActionAssets;

    private IGrabAble currentItem;
    private GameObject objectInterator;

    private bool pickUpitem;


    private void Start()
    {
        SetActions();
        Rb = GetComponent<Rigidbody>();
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
        bool objectInArea = CheckGrabAbleObjects();
        if (objectInArea == false) return;

        objectInterator.transform.position = currentItem.HoldPos.position;
        //objectInterator.transform.rotation = currentItem.HoldPos.rotation; // een check
        currentPickedUpItem.transform.SetParent(objectInterator.transform, false);

        currentItem.HasBeenGrabed(this);
        pickUpitem = true;
    }

    private void TestGrab()
    {
        bool objectInArea = CheckGrabAbleObjects();
        if (objectInArea == false) return;

        objectInterator.transform.position = currentItem.HoldPos.position;
        objectInterator.transform.rotation = currentItem.HoldPos.rotation; // een check
        currentPickedUpItem.transform.SetParent(objectInterator.transform, false);

        currentItem.HasBeenGrabed(this);
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
        if (currentPickedUpItem == null) return;
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
    private void SecondButtonPressed(InputAction.CallbackContext context)
    {
        if (currentItem == null) return;
        if (currentPickedUpItem.TryGetComponent(out IActivateable activatableItem))
        {
            activatableItem.OnPrimaryButton();
        }

    }

    private void Activate(InputAction.CallbackContext context)
    {
        if (currentItem == null || currentPickedUpItem == null) return;
        if (currentPickedUpItem.TryGetComponent(out IActivateable activatableItem))
        {
            if (currentPickedUpItem.TryGetComponent(out AmmoClipFirst ammoClip))
            {
                Debug.Log("It's a ammoClip");

                OnActivatedObject?.Invoke(this);
            }
            Debug.Log("activate");
            activatableItem.Activate();
        }
    }

    private bool CheckGrabAbleObjects()
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
            var leftHandButton = actionMapLeft.FindAction("Button");

            lefthandGrab.performed += Grab;
            lefthandGrab.canceled += Release;
            lefthandActivate.performed += Activate;
            leftHandButton.performed += SecondButtonPressed;


        }
        else
        {
            var actionMapRight = ActionAssets.FindActionMap("XRI RightHand Interaction");
            var righthandGrab = actionMapRight.FindAction("Select");
            var righthanActivate = actionMapRight.FindAction("Activate");
            var rightHandButton = actionMapRight.FindAction("Button");

            righthandGrab.performed += Grab;
            righthandGrab.canceled += Release;
            righthanActivate.performed += Activate;
            rightHandButton.performed += SecondButtonPressed;
        }
    }
}
