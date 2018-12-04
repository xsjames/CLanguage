﻿using CLanguage.Syntax;
using CLanguage.Types;
using System.Linq;

namespace CLanguage.Interpreter
{
    public class EnumContext : EmitContext
    {
        private TypeSpecifier enumTs;
        private CEnumType et;
        private EmitContext emitContext;

        public EnumContext (TypeSpecifier enumTs, CEnumType et, EmitContext emitContext)
            : base (emitContext)
        {
            this.enumTs = enumTs;
            this.et = et;
            this.emitContext = emitContext;
        }

        public override ResolvedVariable ResolveVariable (string name, CType[] argTypes)
        {
            var r = et.Members.FirstOrDefault (x => x.Name == name);
            if (r != null) {
                return new ResolvedVariable ((Value)r.Value, et);
            }

            return base.ResolveVariable (name, argTypes);
        }
    }
}