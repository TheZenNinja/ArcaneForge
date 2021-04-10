using System.Collections.Generic;
using UnityEngine;

namespace Zen.Status
{
    public class StatusManager : MonoBehaviour
    {
        public List<StatusEffect> statuses;



        void FixedUpdate()
        {
            if (statuses.Count > 0)
            {
                for (int i = 0; i < statuses.Count; i++)
                {
                    if (statuses[i].hasDuration)
                    {
                        var s = statuses[i];
                        s.duration -= Time.fixedDeltaTime;
                        statuses[i] = s;
                    }
                }
                statuses.RemoveAll(x => x.hasDuration && x.duration <= 0);
                statuses.RemoveAll(x => x.isStackable && x.stacks <= 0);
            }
        }

        public void AddStatus(StatusEffect status)
        {
            if (statuses.Exists(x => x == status))
            {
                var statusEffect = statuses.Find(x => x == status);
                if (status.duration > statusEffect.duration)
                    statusEffect.duration = status.duration;
            }
            else
            {
                statuses.Add(status);
            }
        }
        public void RemoveStatus(StatusEffectType type, bool completeRemove = true)
        {
            if (completeRemove)
            { 
            
            }
            else 
            { 
            
            }
        }

    }
}