using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabAble
{
    Interactor Interactor { get; set; }
    Transform HoldPos { get; set; }
    Rigidbody Rb { get; set; }

    void SetVariables();
    void HasBeenGrabed();
    void HasBeenReleased();
}
