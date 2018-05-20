using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public static class Util
    {
        public static void Reset(this Transform trans)
        {
            trans.transform.localPosition = Vector3.zero;
            trans.transform.localRotation = Quaternion.identity;
            trans.transform.localScale = Vector3.one;
        }
    }
}

