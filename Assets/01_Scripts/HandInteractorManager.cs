using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInteractorManager : MonoBehaviour
{
    [SerializeField] private Interactor leftHand;
    [SerializeField] private Interactor rightHand;

    [SerializeField] private ShootingObject[] topHand;

    private void Start()
    {
        leftHand.OnActivatedObject += CheckOtherHandForGun;
        rightHand.OnActivatedObject += CheckOtherHandForGun;
    }

    public void CheckOtherHandForGun(Interactor hand)
    {
        if(hand == leftHand)
        {
            if (rightHand.currentPickedUpItem == null) return;
            if (rightHand.currentPickedUpItem.TryGetComponent(out ShootingObject shootAbleObject))
            {
                shootAbleObject.Reload();
            }
        }
        else
        {
            if (leftHand.currentPickedUpItem == null) return;
            if (leftHand.currentPickedUpItem.TryGetComponent(out ShootingObject shootAbleObject))
            {
                shootAbleObject.Reload();
            }
        }
    }


}
