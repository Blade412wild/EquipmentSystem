using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IPlaceAble 
{   
    bool InArea { get; set; }
    Transform PlacementParentTrans { get; set; }

    void PlaceItem();
}
