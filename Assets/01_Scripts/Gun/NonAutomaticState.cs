using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonAutomaticState : State<Gun>
{
    public NonAutomaticState(Gun owner) : base(owner)
    {

    }
}
