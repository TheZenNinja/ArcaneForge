using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zen.Status
{
    [Serializable]
    public struct StatusEffect
    {
        public StatusEffectType type;
        public bool hasDuration;
        public float duration;
        public bool isStackable;
        public int stacks;

        public StatusEffect(StatusEffectType type)
        {
            this.type = type;
            this.hasDuration = false;
            this.isStackable = false;
            this.duration = 0;
            this.stacks = 0;
        }
        public StatusEffect(StatusEffectType type, float duration)
        {
            this.type = type;
            this.hasDuration = true;
            this.duration = duration;
            this.isStackable = false;
            this.stacks = 0;
        }
        public StatusEffect(StatusEffectType type, int stacks)
        {
            this.type = type;
            this.hasDuration = false;
            this.duration = 0;
            this.isStackable = true;
            this.stacks = stacks;
        }
        public StatusEffect(StatusEffectType type, float duration, int stacks)
        {
            this.type = type;
            this.hasDuration = true;
            this.duration = duration;
            this.isStackable = true;
            this.stacks = stacks;
        }

        public static bool operator ==(StatusEffect a, StatusEffect b) => a.type == b.type;
        public static bool operator !=(StatusEffect a, StatusEffect b) => a.type != b.type;
    }
}
