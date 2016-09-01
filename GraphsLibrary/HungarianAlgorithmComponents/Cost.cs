using System;
using GraphsLibrary.Utility;

namespace GraphsLibrary.HungarianAlgorithmComponents
{
    public class Cost : IComparable<Cost>
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int Value { get; set; }
        public Enums.CostType Type { get; set; } = Enums.CostType.Unequivocal;

        public int CompareTo(Cost other)
        {
            if (Row.CompareTo(other.Row) != 0)
            {
                return Row.CompareTo(other.Row);
            }
            if (Column.CompareTo(other.Column) != 0)
            {
                return Column.CompareTo(other.Column);
            }
            if (Value.CompareTo(other.Value) != 0)
            {
                return Value.CompareTo(other.Value);
            }
            if (Type.CompareTo(other.Type) != 0)
            {
                return Type.CompareTo(other.Type);
            }

            return 0;
        }

        public override string ToString()
        {
            string resultString = default(string);
            var type = GetType();
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                resultString += property.Name + ": ";
                resultString += property.GetValue(this, null) + "\n";
            }

            return resultString;
        }
    }
}