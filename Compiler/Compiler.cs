using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;

namespace Compiler
{
    public static class Compiler
    {
        public static Assembly Compile(string code, string libraryReference)
        {
            CSharpCodeProvider provider = new CSharpCodeProvider();

            CompilerParameters parameters = new CompilerParameters();

            parameters.ReferencedAssemblies.Add(libraryReference);

            parameters.GenerateInMemory = true;

            parameters.GenerateExecutable = false;

            CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);

            if (results.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder();

                foreach (CompilerError error in results.Errors)
                {
                    sb.AppendLine(error.ErrorText);
                }

                throw new InvalidOperationException(sb.ToString());
            }

            return results.CompiledAssembly;
        }
    }
}
