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
            /*string ex = "";

                ex += "1";

                //CSharpCodeProvider provider = new CSharpCodeProvider();

                Assembly system = Assembly.LoadFile(systemReference);

                ex += "2";

                //var codeProviderType = system.GetType("Microsoft.CSharp.CSharpCodeProvider");

                ex += "3";
                var codeDomProviderType = system.GetType("System.CodeDom.Compiler.CodeDomProvider");

                ex += "4";

                //var provider = Activator.CreateInstance(codeProviderType);

                if (codeDomProviderType == null) ex += "null";

               var methods = codeDomProviderType.GetMethods();

                if (codeDomProviderType.GetMethod("CreateProvider", BindingFlags.Static) == null) ex += "aaa";

                ex += "y";

                var provider = codeDomProviderType.GetMethod("CreateProvider", BindingFlags.Static).Invoke(null, new object[] { "CSharp" });
                ex += "5";

                CompilerParameters parameters = new CompilerParameters();
                ex += "6";

                parameters.ReferencedAssemblies.Add(libraryReference);
                ex += "7";

                parameters.GenerateInMemory = true;
                ex += "8";

                parameters.GenerateExecutable = false;
                ex += "9";

                //CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);

                CompilerResults results = codeDomProviderType.GetMethod("CompileAssemblyFromSource").Invoke(provider, new object[] { parameters, code }) as CompilerResults;
                ex += "x";

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

                return results.CompiledAssembly;*/
            
       
            CSharpCodeProvider provider = new CSharpCodeProvider();

            CompilerParameters parameters = new CompilerParameters();

            parameters.ReferencedAssemblies.Add(libraryReference);

            parameters.GenerateInMemory = true;

            parameters.GenerateExecutable = false;

            var compiler = new CSharpCodeCompilerMono.CSharpCodeCompiler();

            CompilerResults results = compiler.CompileAssemblyFromSource(parameters, code);

            //CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);

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
