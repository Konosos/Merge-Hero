using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField]protected CharController charController;
    public bool attacking = false;
    [SerializeField] private float attackTime = 0.3f;
    public IEnumerator Attacking(GameObject _targetObj)
    {
        //characterAnimator.AttackState();
        attacking = true;
        yield return new WaitForSeconds(attackTime);
        Attack(_targetObj);
        attacking = false;
    }
    private void Attack(GameObject _targetObj)
    {

        if (_targetObj == null)
            return;
    }
}
