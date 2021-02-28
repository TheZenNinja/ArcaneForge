using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
public class Version : IComparable<Version>
#pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning restore CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
{
    public int version;
    public int update;
    public int patch;

    public Version(int version, int update = 0, int patch = 0)
    {
        this.version = version;
        this.update = update;
        this.patch = patch;
    }
    public Version(string version)
    {
        try
        {
            string[] s = version.Split('.');
            this.version =  int.Parse(s[0]);
            this.update =   int.Parse(s[1]);
            this.patch =    int.Parse(s[2]);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogWarning($"Could not read version: {version}\n{e.Message}");
            this.version = 0;
            this.update = 0;
            this.patch = 0;
        }
    }
    public override string ToString()
    {
        return $"{version}.{update}.{patch}";
    }
    public int CompareTo(Version other)
    {
        int compare = version.CompareTo(other.version);
        if (compare != 0)
            return compare;

        compare = update.CompareTo(other.update);
        if (compare != 0)
            return compare;

        compare = patch.CompareTo(other.patch);
        UnityEngine.Debug.Log(compare);
        return compare;
    }

    public static bool operator >(Version a, Version b) => a.CompareTo(b) > 0;
    public static bool operator <(Version a, Version b) => a.CompareTo(b) < 0;
    public static bool operator ==(Version a, Version b) => a.CompareTo(b) == 0;
    public static bool operator !=(Version a, Version b) => a.CompareTo(b) != 0;
}
