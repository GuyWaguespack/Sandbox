using System;
using System.Diagnostics;

using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;

namespace AmazonWebServices
{
    public class Utils
    {
        public static AWSCredentials LoadProfile()
        {
            string profileName = Environment.GetEnvironmentVariable("AWS_DEFAULT_PROFILE");
            if (String.IsNullOrWhiteSpace(profileName))
                profileName = "default";

            return LoadProfile(profileName);
        }

        public static AWSCredentials LoadProfile(string profileName)
        {
            CredentialProfileStoreChain chain = new CredentialProfileStoreChain();
            CredentialProfile profile;

            chain.TryGetProfile(profileName, out profile);
            if (!chain.TryGetProfile(profileName, out profile))
            {
                chain.TryGetProfile("default", out profile);
            }

            Console.WriteLine($"Using Profile [{profile.Name}]");
            return profile.GetAWSCredentials(null);
        }

        public static string CurrentMethod {
            get {
                StackTrace st = new StackTrace();
                StackFrame sf = st.GetFrame(1);
                System.Reflection.MethodBase method = sf.GetMethod();
                return "\n" + method.ReflectedType.Name + "." + method.Name + " (" + method.Module.Name + ")";
            }
        }


    }
}
