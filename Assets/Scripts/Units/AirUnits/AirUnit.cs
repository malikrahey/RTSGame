using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirUnit : Unit
{

    protected float elevationHeight;

    protected override IEnumerator MoveToPositionCoroutine(Vector3 position)
    {
        position = new Vector3(position.x, position.y + elevationHeight, position.z);
        while (Vector3.Distance(position, this.gameObject.transform.position) > 0.05)
        {
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, position, BaseSpeed / 1000);
            yield return null;
        }
    }

    protected override IEnumerator MoveToTargetCoroutine(Target target, float range)
    {
        Vector3 position = new Vector3(target.Position.x, target.Position.y + elevationHeight, target.Position.z);
        while (Vector3.Distance(position, transform.position) > range)
        {
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, position, BaseSpeed / 1000);
            yield return null;
        }
    }
}
