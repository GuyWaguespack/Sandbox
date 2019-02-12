using System;

using Sandbox.Lambda;

namespace Sandbox.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            SandboxLambda lam = new SandboxLambda();
            lam.ToUpperLambda("Hello World", null);
        }
    }
}
