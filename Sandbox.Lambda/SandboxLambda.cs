using System;

using Sandbox.Core;

using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace Sandbox.Lambda
{
    public class SandboxLambda
    {
        public string ToUpperLambda(string input, ILambdaContext ctx)
        {
            LambdaLogger.Log($">>> Input  : {input}");
            SandboxCore syn = new SandboxCore("GuysTest");
            string rc = syn.ToUpper(input);
            LambdaLogger.Log($">>> Output : {rc}");
            return rc;
        }
    }
}
