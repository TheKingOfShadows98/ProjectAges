using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Tk.ProyectAge
{
    public class NpcController : MonoBehaviour
    {
        public TeamColor Team;
        public float speed  = 5f;
        public int MaxLife  = 0;
        public int Life  = 0;
        public int baseAttack = 0;
        public int Attack = 0;
        public float Range;
        public float attackSpeed = 1;
        public int defense = 0;
        public GameObject Destino;
        public NpcController Target;
        public Animator anim;
        [SerializeField]
        private SphereCollider col_range;
        public Action accion;
        private float timeAtack;

    private void OnEnable()
    {
            accion = Caminar;
            //anim.SetBool("walking", true);
    }
    // Update is called once per frame
    void Update()
    {
            accion();
    }

    void Caminar()
        {
            if (Destino != null)
            {
                Vector3 direction = Destino.transform.position - transform.position;
                direction = direction.normalized;
                transform.rotation = Quaternion.LookRotation(direction);
                transform.Translate(Vector3.forward * (speed * Time.deltaTime));

                if (Vector3.Distance(transform.position, Destino.transform.position) < 0.5f)
                {

                    Die();
                }
            }
            

        }
    void atacar()
        {
            if(timeAtack <= 0)
            {
                Target.TakeDamage(Attack);
                timeAtack = 1 / attackSpeed;
            }
            else
            {
                timeAtack -= Time.deltaTime;
            }
            
            if (!Target.gameObject.activeSelf)
            {
                accion = Caminar;
                timeAtack = 0;
               // anim.SetBool("atacking", false);
               // anim.SetBool("walking", true);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            NpcController enemy;
            if (!other.transform.CompareTag(gameObject.tag) && other.transform.TryGetComponent<NpcController>(out enemy) && !other.isTrigger)
            {
                accion = atacar;
                Target = enemy;
               // anim.SetBool("walking", false);
               // anim.SetBool("atacking", true);
            }
        }

    public void TakeDamage( int damage)
        {
            Life -= damage;
            if(Life <= 0)
            {
                Die();
            }
        }
    void Die()
    {
        //anim.SetBool("walking", false);
        //anim.SetBool("atacking", false);
        PoolerSystem.SINGLETON.addToPool(gameObject);
        gameObject.SetActive(false);
    }
    public void Init()
        {
            Life = MaxLife;
            Attack = baseAttack;
            //anim.SetBool("walking", true);
            //anim.SetBool("atacking", false);
            switch (Team)
            {
                case TeamColor.Azul:
                    {
                        gameObject.tag = "Player1";
                        break;
                    }
                case TeamColor.Rojo:
                    {
                        gameObject.tag = "Player2";
                        break;
                    }
                default:
                    break;
            }
            if (col_range)
            {
                col_range.radius = Range;
            }
        }
    }


}