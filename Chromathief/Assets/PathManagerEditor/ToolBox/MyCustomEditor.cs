using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ID.ToolBox.CustomEditor
{
    public abstract class MyCustomEditor<T> : Editor where T : MonoBehaviour
    {
        protected T eTarget = default(T);

        protected virtual void OnEnable()
        {
            eTarget = (T)target;
            eTarget.name = $"[{typeof(T).Name.ToUpper()} TOOL]";
        }
    }
}
