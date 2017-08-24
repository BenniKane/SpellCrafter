using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.SpellFramework
{
    public interface ISpellCaster
    {
        Quaternion LookRotation
        {
            get;
        }

        Vector3 Position
        {
            get;
        }

        void ReceiveSpell(MasterSpell spell);
    }
}
