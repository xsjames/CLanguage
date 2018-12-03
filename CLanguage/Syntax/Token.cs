﻿using System;
using System.Collections.Generic;

namespace CLanguage.Syntax
{
    public struct Token : IEquatable<Token>
    {
        public readonly int Kind;
        public readonly Location Location;
        public readonly Location EndLocation;
        public readonly object Value;

        public Token (int kind, object value, Location location, Location endLocation)
        {
            Kind = kind;
            Value = value;
            Location = location;
            EndLocation = endLocation;
        }

        public string Text => Location.IsNull || EndLocation.IsNull || Location.Document.Path != EndLocation.Document.Path ? "" :
            Location.Document.Content.Substring (Location.Index, EndLocation.Index - Location.Index);

        public override string ToString () => $"\"{Text}\": {Parser.CParser.yyname (Kind)}";

        public override bool Equals (object obj) => obj is Token && Equals ((Token)obj);

        public bool Equals (Token other)
        {
            return Kind == other.Kind &&
                   Location.Equals (other.Location) &&
                   EqualityComparer<object>.Default.Equals (Value, other.Value);
        }

        public override int GetHashCode ()
        {
            var hashCode = 666775603;
            hashCode = hashCode * -1521134295 + Kind.GetHashCode ();
            hashCode = hashCode * -1521134295 + EqualityComparer<Location>.Default.GetHashCode (Location);
            hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode (Value);
            return hashCode;
        }

        public static bool operator == (Token token1, Token token2)
        {
            return token1.Equals (token2);
        }

        public static bool operator != (Token token1, Token token2)
        {
            return !(token1 == token2);
        }
    }
}
