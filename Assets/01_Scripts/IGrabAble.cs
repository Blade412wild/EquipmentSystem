using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabAble
{
    Transform HoldPos { get; set; }
    Rigidbody Rb { get; set; }

    void HasBeenGrabed();
    void HasBeenReleased();
}
