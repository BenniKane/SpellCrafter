using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.SpellFramework.SpellNodes
{
    public interface ISpellNode
    {
        string Name { get; }

        SpellNodeType NodeType { get; }

        void AppendToSpell(Subspell parentObject);
    }
}
