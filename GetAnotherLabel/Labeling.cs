using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetAnotherLabel
{
    public class Labeling
    {
        public string Label { get; protected set; }
        public string AnnotatorId { get; protected set; }
        public string ObjectId { get; protected set; }

        public Labeling(string a, string o, string c)
        {
            this.AnnotatorId = a;
            this.ObjectId = o;
            this.Label = c;
        }

        public override bool Equals(object obj)
        {
            if (obj is Labeling) {
                Labeling p = (Labeling)obj;
                if (p.AnnotatorId == this.AnnotatorId && p.ObjectId == this.ObjectId) {
                    return true;
                }
                return false;
            }
            throw new InvalidCastException();
        }
        public override int GetHashCode()
        {
            return this.AnnotatorId.GetHashCode() + this.ObjectId.GetHashCode() + this.Label.GetHashCode();
        }
    }
}
