using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Code.SpellFramework.DeliveryMethods.Scriptables;
using System.Collections;
using UnityEngine;

namespace Assets.Code.SpellFramework.DeliveryMethods.DeliveryMethodConcretes
{
    public class PointBlankAreaOfEffectDeliveryMethod : IDeliveryMethod
    {
        private float currentTimeAlive = 0f;
        private float lifetime = 3f;
        private Spell owningSpell;

        public EDeliveryMethods DeliveryMethod
        {
            get
            {
                return EDeliveryMethods.PBAoE;
            }
        }

        public void Activate(Spell parentObject)
        {
            owningSpell = parentObject;

            owningSpell.StartCoroutine(ProjectSpell());
        }

        private IEnumerator ProjectSpell()
        {
            while (currentTimeAlive <= lifetime)
            {
                currentTimeAlive += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            UnityEngine.Object.Destroy(owningSpell.gameObject);
        }
    }
}
