using Assets.Code.GeneralFramework;
using Assets.Code.SpellFramework.SpellTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.SpellFramework
{
    public class SpellTarget : MonoBehaviour, ISpellTarget
    {
        [SerializeField]
        private float currentHealth = 50;

        private float maxHealth = 50;

        private Dictionary<ElementalType, float> resistancePercentage;

        public void Awake()
        {
            resistancePercentage = new Dictionary<ElementalType, float>();

            for (ElementalType i = ElementalType.Fire; i <= ElementalType.None; i++)
            {
                resistancePercentage.Add(i, 0f);
            }

            currentHealth = maxHealth;
        }

        public bool TakeDamage(float damageVal, ElementalType damageType = ElementalType.None)
        {
            currentHealth -= damageVal - (damageVal * resistancePercentage[damageType]);

            if(currentHealth <= 0)
            {
                NotifyOfDeath();
                Destroy(gameObject);
                return true;
            }

            return false;
        }

        private void NotifyOfDeath()
        {
            IOnDeathNotify[] deathNotifies = transform.root.GetComponentsInChildren<IOnDeathNotify>();

            for(int i = 0; i < deathNotifies.Length; i++)
            {
                deathNotifies[i].OnNotificationOfDeath();
            }
        }
    }
}
