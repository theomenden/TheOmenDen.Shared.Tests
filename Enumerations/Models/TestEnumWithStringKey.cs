using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheOmenDen.Shared.Tests.Enumerations.Models
{
    public sealed record TestEnumWithStringKey: EnumerationBase<TestEnumWithStringKey, String>
    {
        public static readonly TestEnumWithStringKey Option1 = new (nameof(Option1), nameof(Option1));
        public static readonly TestEnumWithStringKey Option2 = new (nameof(Option2), nameof(Option2));
        public static readonly TestEnumWithStringKey Option3 = new (nameof(Option3), nameof(Option3));

        private TestEnumWithStringKey(string name, string value) : base(name, value)
        {
        }
    }
}
