using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheOmenDen.Shared.Tests.Enumerations.Models
{
    internal record TestDerivedEnum: TestEnumeration
    {
        public static TestDerivedEnum DerivedOption1 = new (nameof(DerivedOption1), 1);
        public static TestDerivedEnum DerivedOption2 = new (nameof(DerivedOption2), 2);
        public static TestDerivedEnum DerivedOption3 = new (nameof(DerivedOption3), 3);

        private TestDerivedEnum(string name, int id) : base(name, id)
        {
        }
    }
}
