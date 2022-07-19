using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAI : MonoBehaviour
{
    [SerializeField] private GameObject targetObj = null;
    [SerializeField]
    private CharController charController;
    public float radius = 5f;
    public LayerMask targetMask;
    public bool attacking = false;
    [SerializeField] private float attackTime = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isStarted)
            return;
        if (charController.characterInformation.isDeath)
            return;
        AttackEnemy();
    }
    void AttackEnemy()
    {
        if (targetObj == null) 
        {
             FindEnemy();
        }
        else
        {
            Vector3 disPos = targetObj.transform.position;

            if (!attacking)
            {
                StartCoroutine(Attacking());
            }
        }
        
    }
    private void FindEnemy()
    {

        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        if (rangeChecks.Length == 0)
            return;
        //Debug.Log(rangeChecks.Length);
        float distance = 100000f;
        foreach (Collider obj in rangeChecks)
        {
            if (obj.gameObject.tag == this.gameObject.tag)
                continue;

            Transform target = obj.transform;
            Vector3 directionToTarget = target.position - transform.position;

            if (directionToTarget.magnitude < distance)
            {
                targetObj = obj.gameObject;
                
                distance = directionToTarget.magnitude;
                
            }

        }

    }
    public IEnumerator Attacking()
    {
        //characterAnimator.AttackState();
        attacking = true;
        yield return new WaitForSeconds(attackTime);
        Attack();
        attacking = false;
    }
    private void Attack()
    {
        if (targetObj == null)
            return;
        CharacterInformation charInfor = targetObj.GetComponent<CharacterInformation>();
        charInfor.TakeDamege(charController.characterInformation.atk);
    }
}
