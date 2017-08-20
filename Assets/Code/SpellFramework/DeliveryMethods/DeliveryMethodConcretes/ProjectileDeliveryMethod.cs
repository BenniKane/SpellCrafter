﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Code.SpellFramework.DeliveryMethods.Scriptables;
using UnityEngine;

namespace Assets.Code.SpellFramework
{
    public class ProjectileDeliveryMethod : IDeliveryMethod
    {
        private float travelTime = 5.0f;
        private float currentTravelTime = 0f;

        private Spell owningSpell;

        public EDeliveryMethods DeliveryMethod
        {
            get
            {
                return EDeliveryMethods.Projectile;
            }
        }

        public void Activate(Spell parentObject)
        {
            owningSpell = parentObject;

            owningSpell.GetComponent<Rigidbody>().AddForce(owningSpell.transform.forward * 875f);
            owningSpell.StartCoroutine(ProjectSpell());

        }

        private IEnumerator ProjectSpell()
        {
            while(currentTravelTime <= travelTime)
            {
                currentTravelTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            UnityEngine.Object.Destroy(owningSpell.gameObject);
        }
    }
}