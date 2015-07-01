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
                var exception = new InvalidOperationException();

                var errors = new List<string>();

                foreach (CompilerError error in results.Errors)
                {
                    errors.Add(error.Line.ToString() + " @ " + error.Column.ToString() + " @ " + error.ErrorText);
                }
                
                exception.Data["Errors"] = errors;

                throw exception;
            }

            return results.CompiledAssembly;
        }
    }
}
