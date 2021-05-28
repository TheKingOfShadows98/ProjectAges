using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tk.ProyectAge
{

    public class UnitController : MonoBehaviour
    {
        public UnitData data;
        public Transform destination;
        public Material[] teamMaterials;
        public System.Action aa;

        public UnitController target;

        public void FixedUpdate()
        {
            aa?.Invoke();
        }

        private void Atack()
        {
        }

        private void Walk()
        {
                if (destination != null)
                {
                    Vector3 direction = destination.position - transform.position;
                    direction = direction.normalized;
                    transform.rotation = Quaternion.LookRotation(direction);
                    transform.Translate(Vector3.forward * (data.speed * Time.deltaTime));

                    if (Vector3.Distance(transform.position, destination.position) <data.attackRange)
                    {

                        Atack();
                    }
                }

        }

        private void Die()
        {
            transform.parent.gameObject.SetActive(false);
        }
        public void TakeDamage(int damage)
        {
            damage = (damage - data.defense < 1 )? damage - data.defense :  1;
            data.Life -= damage;
            if(data.Life < 1)
            {
                Die();
            }
        }
        public void init()
        {
            if(data != null)
            {
                data.Life = data.maxLife;
                data.attack = data.baseAttack;
                aa = Walk;
            }
            else
            {
                Die();
            }
        }
    }

}