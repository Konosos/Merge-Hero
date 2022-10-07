using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckCharacterDeath : DecoratorNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if(!context.charController.characterInformation.isDeath)
        {
            return child.Update();
        }
        else
        {
            return State.Failure;
        }
        //return State.Success;
    }
}
