using Assets.Code.SpellFramework.DeliveryMethods.Scriptables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.SpellFramework
{
    public interface IDeliveryMethod
    {
        EDeliveryMethods DeliveryMethod { get; }


        void Activate(Spell parentObject);
    }
}
