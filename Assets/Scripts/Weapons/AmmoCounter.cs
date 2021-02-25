using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weapons
{
    public struct AmmoCounter
    {
        public int max;
        public int current;
        public bool isFull => current >= max;
        public bool isEmpty => current <= 0;

        public AmmoCounter(int max)
        {
            current = 0;
            this.max = max;
        }
        public void Reload()
        {
            current = max;
        }
        public static AmmoCounter operator +(AmmoCounter a, int i)
        {
            a.current = UnityEngine.Mathf.Clamp(a.current + i, 0, int.MaxValue);
            return a;
        }
        public static AmmoCounter operator -(AmmoCounter a, int i)
        {
            a.current = UnityEngine.Mathf.Clamp(a.current - i, 0, int.MaxValue);
            return a;
        }
        public static AmmoCounter operator ++(AmmoCounter a) => a + 1;
        public static AmmoCounter operator --(AmmoCounter a) => a - 1;
    }
}
