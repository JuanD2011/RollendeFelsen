using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    protected override void Move()
    {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator Push()
    {
        yield return null;
    }

    protected override void Stun()
    {
        throw new System.NotImplementedException();
    }
}
