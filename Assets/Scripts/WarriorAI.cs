using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WarriorAI : MonoBehaviour
{
    [SerializeField]
    private CharController charController;
    private NavMeshAgent nav;
    public bool isMove = false;
   
    private GameObject targetObj=null;

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
        if (!GameManager.instance.isStarted)
            return;
        if (charController.characterInformation.isDeath)
            return;
        MoveToEnemy();
    }
    private void MoveToEnemy()
    {
        
        if(targetObj==null)
        {
             FindEnemy();
            return;
        }
            
        Vector3 disPos = targetObj.transform.position;
        if (isMove)
        {
            
            nav.SetDestination(disPos);
        }
        Vector3 distanceToWalkPoint = transform.position - disPos;
        
        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1.5f)
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
        if (targetObj == null)
            return;
        CharacterInformation charInfor=targetObj.GetComponent<CharacterInformation>();
        charInfor.TakeDamege(charController.characterInformation.atk);
    }
    private void OnEnable()
    {
        EvenManager.OnClicked += TurnOnNav;
    }
    private void OnDisable ()
    {
        EvenManager.OnClicked -= TurnOnNav;
    }
    private void TurnOnNav()
    {
        nav.enabled = true;
    }
}
