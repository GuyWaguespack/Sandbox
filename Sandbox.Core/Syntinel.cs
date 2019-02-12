using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sandbox.Core
{
    public class SandboxCore
    {
        public string MyClassName { get; internal set; }

        public SandboxCore()
        {
            MyClassName = "DefaultName";
        }

        public SandboxCore(string name)
        {
            MyClassName = name;
        }

        public string ToUpper(string str)
        {
            return str?.ToUpper() + " | " + MyClassName;
        }
    }
}
