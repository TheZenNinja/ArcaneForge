using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace Crafting
{
    [Serializable]
    public struct PartID : IComparable<PartID>, IEquatable<PartID>
    {
        public PartType type;
        public int subtype;
        public PartID(PartType type, int subtype = 0)
        {
            this.type = type;
            this.subtype = subtype;
        }
        public PartID(BladeSubtype subtype)
        {
            this.type = PartType.blade;
            this.subtype = (int)subtype;
        }
        public PartID(HandleSubtype subtype)
        {
            this.type = PartType.handle;
            this.subtype = (int)subtype;
        }
        public PartID(MiscPartSubtype subtype)
        {
            this.type = PartType.misc;
            this.subtype = (int)subtype;
        }
        public int CompareTo(PartID other)
        {
            int typeTest = type.CompareTo(other.type);
            if (typeTest != 0)
                return typeTest;
            return subtype.CompareTo(other.subtype);
        }
        public string GetCleanName()
        {
            switch (type)
            {
                case PartType.handle:
                    {
                        var sub = (HandleSubtype)subtype;
                        switch (sub)
                        {
                            case HandleSubtype.oneHand:
                                return "One Hand Handle";
                            case HandleSubtype.twoHand:
                                return "Two Hand Handle";
                            case HandleSubtype.staff:
                                return "Staff Handle";
                            default:
                                return sub.ToString().Capitalize();
                        }
                    }
                case PartType.blade:
                    {
                        var sub = (BladeSubtype)subtype;
                        switch (sub)
                        {
                            case BladeSubtype.singleEdge:
                                return "Single Edge Blade";
                            case BladeSubtype.largeBlade:
                                return "Large Blade";
                            case BladeSubtype.shortBlade:
                                return "Short Blade";
                            case BladeSubtype.sword:
                                return "Sword Blade";
                            default:
                                return sub.ToString().Capitalize();
                        }
                    }
                case PartType.misc:
                    {
                        var sub = (MiscPartSubtype)subtype;
                        switch (sub)
                        {
                            case MiscPartSubtype.guard:
                                return "Sword Guard";
                            case MiscPartSubtype.ornament:
                            default:
                                return sub.ToString().Capitalize();
                        }
                    }
                default:
                    throw new System.ArgumentOutOfRangeException($"{type} isnt an implimented type");
            }
        }
        public override string ToString()
        {
            string name = "";
            switch (type)
            {
                case PartType.handle:
                    string s = ((HandleSubtype)subtype).ToString();
                    name += s.Capitalize();
                    name += "Handle";
                    break;
                case PartType.blade:
                    s = ((BladeSubtype)subtype).ToString();
                    name += s.Capitalize();
                    name += "Blade";
                    break;
                case PartType.misc:
                    s = ((MiscPartSubtype)subtype).ToString();
                    name += s;
                    break;
            }

            return name;
        }

        public bool Equals(PartID other) => type == other.type && subtype == other.subtype;
    }


}
