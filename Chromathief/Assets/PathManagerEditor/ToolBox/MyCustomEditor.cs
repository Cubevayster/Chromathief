using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ID.ToolBox.CustomEditor
{
#if UNITY_EDITOR
    public abstract class MyCustomEditor<T> : Editor where T : MonoBehaviour
    {
        protected T eTarget = default(T);

        protected virtual void OnEnable()
        {
            eTarget = (T)target;
            eTarget.name = $"[{typeof(T).Name.ToUpper()} TOOL]";
        }
    }
#endif
}
