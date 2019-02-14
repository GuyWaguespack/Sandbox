using System;
using System.Diagnostics;

namespace AmazonWebServices
{
    public class Utils
    {
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
