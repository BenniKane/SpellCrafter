using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Code.SpellFramework.SpellNodes;
using UnityEngine;
using Assets.Code.SpellFramework.DeliveryMethods.DeliveryMethodConcretes;

namespace Assets.Code.SpellFramework.DeliveryMethods.Scriptables
{
    public enum EDeliveryMethods
    {
        Projectile,
        Self,
        Raycast,
        Cone,
        PBAoE
    }
    
    [CreateAssetMenu(fileName = "DeliveryMethod", menuName = "Create New Delivery method")]
    public class DeliveryMethodScriptable : SpellNodeScriptable
    {
        public EDeliveryMethods deliveryMethod;

        public override ISpellNode GetNode()
        {
            return new DeliveryMethodSpellNode(m_SpellNodeName, m_SpellNodeType, GetDeliveryMethod(), m_ProcessingTime);
        }

        private IDeliveryMethod GetDeliveryMethod()
        {
            switch(deliveryMethod)
            {
                case EDeliveryMethods.PBAoE:
                    return new PointBlankAreaOfEffectDeliveryMethod();
                default:
                    return new ProjectileDeliveryMethod();
            }
        }
    }
}
