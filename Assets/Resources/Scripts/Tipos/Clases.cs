using UnityEngine;
using System.Collections;
namespace Tk.ProyectAge
{
    [System.Serializable]
    public class Equipo_Info
    {
        public Material EMaterial;
        public GameObject Base;
        public string Nombre;
        public Equipo_Info(Material _mat, GameObject _base, string _name)
        {
            EMaterial = _mat;
            Base = _base;
            Nombre = _name;
        }
    }

    [System.Serializable]
    public class UnitData
    {
        public string name;
        public Mesh mesh;
        public int maxLife;
        public int Life;
        public int baseAttack;
        public int attack;
        public int defense;
        public float speed;
        public int moneyCost;
        public float attackRange;
        public float train_time;
        public int attack_multiplier;
        public int defenze_multiplier;
        public UnitData(Mesh _mesh, int _maxLife = 10, int _baseAttack = 1, float _speed = 5f, string _name = "", int _money_cost = 5 ,float _attackRange = 1.5f)
        {
            name = _name;
            mesh = _mesh;
            maxLife = _maxLife;
            baseAttack = _baseAttack;
            speed = _speed;
            moneyCost = _money_cost;
            attackRange = _attackRange;
        }
    }

    public enum TeamColor
    {
        Azul,
        Rojo,
        Verde,
        Naranja
    }

}
