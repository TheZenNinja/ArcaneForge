using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;
public static class ClassExtentions
{

    public static void Play(this AudioSource a, AudioData data)
    {
        a.SetData(data);
        a.Play();
    }
    public static void PlayRandomized(this AudioSource a, float pitchVariation = .1f)
    {
        throw new System.NotImplementedException();
    }
    public static void SetData(this AudioSource a, AudioData data)
    {
        a.clip = data.clip;
        a.volume = data.volume;
    }
    public static string Capitalize(this string s)
    { 
        return s.ToUpper().Substring(0, 1) + s.Substring(1);
    }
    public static string EnumReformat(this string s)
    {
        string output = s;

        Regex rx = new Regex("(?!(^|\\s))[A-Z]");
        MatchCollection matches = rx.Matches(s);

        output = output.Capitalize();

        return output;
    }

    public static bool CollidersWithinBounds(this Collider root, Collider col) => root.CollidersAreWithinBounds(new Collider[] { col });
    public static bool CollidersAreWithinBounds(this Collider root, Collider[] cols)
    {
        foreach (var c in cols)
            if (root.bounds.Intersects(c.bounds))
                return true;

        return false;
    }

    #region List
    public static List<T> Shuffle<T>(this List<T> list)
    {
        List<T> randomList = new List<T>();
        System.Random rnd = new System.Random();
        int i = 0;
        while (list.Count > 0)
        {
            i = rnd.Next(0, list.Count);
            randomList.Add(list[i]);
            list.RemoveAt(i);
        }
        return randomList;
    }
    #endregion

    #region Vector
    public static Vector3 randomInBounds(this Bounds b)
    {
        Vector3 v = Vector3.zero;

        v.x = b.center.x + Random.Range(-b.extents.x, b.extents.x);
        v.y = b.center.y + Random.Range(-b.extents.y, b.extents.y);
        v.z = b.center.z + Random.Range(-b.extents.z, b.extents.z);

        return v;
    }
    public static Vector3 toV3(this Vector2 v) => new Vector3(v.x, 0, v.y);
    public static Vector3 InverseLerp(this Vector3 from, Vector3 to, float t)
    {
        Vector3 v = from;
        v.x = Mathf.InverseLerp(v.x, to.x, t);
        v.y = Mathf.InverseLerp(v.y, to.y, t);
        v.z = Mathf.InverseLerp(v.z, to.z, t);
        return v;
    }
    public static Vector3 LerpXZ(this Vector3 from, Vector3 to, float t)
    {
        float y = from.y;
        Vector3 v = Vector3.Lerp(from, to, t);
        v.y = y;
        return v;
    }
    public static Vector3 RelativeForward(this Vector3 rootPos, Transform goal, float maxDistance = 100)
    {
        RaycastHit hit;
        if (Physics.Raycast(goal.position, goal.forward, out hit, maxDistance))
            return (hit.point - rootPos).normalized;

        return goal.forward;
    }
    public static Vector2 FromRotation(float angle) => new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
    #endregion
}
