using UnityEngine;

public static class Vector3Extensions
{
    public static Vector3 With(this Vector3 originalVector, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3( x ?? originalVector.x, y ?? originalVector.y, z ?? originalVector.z);
    }

}
