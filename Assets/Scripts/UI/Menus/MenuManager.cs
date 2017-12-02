using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LD40.UI.Menus
{
    public class MenuManager : MonoBehaviour
    {
        public T Pop<T>() where T : Menu //T here referes to any type. functions like getcomponent<type> work the same way. 
            //its like normal arguments, but instead of values we are using actual class types
            //the where T : menu means that it only accepts types that are Menus.
        {
            //we need to get the prefab from resources. 

            string name = typeof(T).ToString();

            UnityEngine.Object[] prefabs = Resources.LoadAll("Menus"); //still should actually only be one type

            GameObject obj = Instantiate(prefabs[0], Game.GetCanvas().transform) as GameObject; //should work?
            return obj.GetComponent<T>();
        }
    }
}
