using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityForce : Force
{
    [SerializeField] FloatData gravity;

    public override void ApplyForce(List<Body> bodies)
    {
        //can change Vector2.down if remove -20 from gravity
        bodies.ForEach(body => body.ApplyForce(Vector2.up * gravity.value, Body.eForceMode.ACCELERATION));
    }
}
