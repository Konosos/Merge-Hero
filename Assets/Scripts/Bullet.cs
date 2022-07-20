using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 dir;
    public int dame;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyMySelf", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * Time.deltaTime * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == this.tag)
            return;
        CharacterInformation charInfor = other.GetComponent<CharacterInformation>();
        charInfor.TakeDamege(dame);
        Destroy(this.gameObject);
    }
    void DestroyMySelf()
    {
        Destroy(this.gameObject);
    }
}
