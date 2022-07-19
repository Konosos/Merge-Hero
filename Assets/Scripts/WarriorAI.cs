using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WarriorAI : MonoBehaviour
{
    [SerializeField]private NavMeshAgent nav;
    public bool isMove = false;
    [SerializeField]private Vector3 disPos;
    [SerializeField]private GameObject targetObj=null;

    public float radius=5f;
    public LayerMask targetMask;
    public bool attacking = false;
    [SerializeField] private float attackTime = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Patroling();
    }
    private void Patroling()
    {
        
        if(targetObj==null) FindEnemy();

        if (isMove)
        {
            
            nav.SetDestination(targetObj.transform.position);
        }
        Vector3 distanceToWalkPoint = transform.position - disPos;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 2.5f)
        {
            isMove = false;
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
        float distance=100000f;
        foreach (Collider obj in rangeChecks)
        {
            if (obj.gameObject.tag == this.gameObject.tag)
                continue;
            
            Transform target = obj.transform;
            Vector3 directionToTarget = target.position - transform.position;
            
            if (directionToTarget.magnitude<distance)
            {
                targetObj=obj.gameObject;
                disPos=target.position;
                distance=directionToTarget.magnitude;
                isMove = true;
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
        CharacterInformation charInfor=targetObj.GetComponent<CharacterInformation>();
        charInfor.TakeDamege(2);
    }
}
