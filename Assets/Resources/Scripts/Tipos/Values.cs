using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Tk.ProyectAge
{
    public class Values : MonoBehaviour
    {

        public static Dictionary<TeamColor, Equipo_Info> Equipos = new Dictionary<TeamColor, Equipo_Info>();
        public Material[] _values = new Material[2];
        public GameObject[] Bases = new GameObject[2];
        public string[] names = new string[2];

        void Awake()
        {
            Equipos.Add(TeamColor.Azul, new Equipo_Info(_values[0], Bases[0], names[0]));
            Equipos.Add(TeamColor.Rojo, new Equipo_Info(_values[1], Bases[1], names[1]));
        }


    }
   
}