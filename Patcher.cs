using System;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

class Patcher
{
    static void Main(string[] args)
    {
        try {
            string gameDir = args[0];
            string managedDir = gameDir + @"\Rise of Industry_Data\Managed";
            string assemblyPath = managedDir + @"\Assembly-CSharp.dll";
            string backupPath = assemblyPath + ".bak";

            if (!System.IO.File.Exists(backupPath))
            {
                System.IO.File.Copy(assemblyPath, backupPath);
            }

            var resolver = new DefaultAssemblyResolver();
            resolver.AddSearchDirectory(managedDir);

            var parameters = new ReaderParameters { AssemblyResolver = resolver, ReadWrite = true };
            using (var asmDef = AssemblyDefinition.ReadAssembly(assemblyPath, parameters))
            {
                var sunType = asmDef.MainModule.Types.FirstOrDefault(t => t.FullName == "ProjectAutomata.Sun");
                if (sunType == null) {
                    Console.WriteLine("Could not find Sun type");
                    return;
                }

                var updateMethod = sunType.Methods.FirstOrDefault(m => m.Name == "Update");
                if (updateMethod == null) {
                    Console.WriteLine("Could not find Update method");
                    return;
                }

                // Check if already patched
                bool alreadyPatched = updateMethod.Body.Instructions.Any(i => i.OpCode == OpCodes.Call && i.Operand.ToString().Contains("ApplyNight"));
                if (alreadyPatched) {
                    Console.WriteLine("Already patched!");
                    return;
                }

                var hookAsmDef = AssemblyDefinition.ReadAssembly(managedDir + @"\AlwaysNightHook.dll", parameters);
                var hookType = hookAsmDef.MainModule.Types.FirstOrDefault(t => t.FullName == "AlwaysNightHook");
                var applyNightMethod = hookType.Methods.FirstOrDefault(m => m.Name == "ApplyNight");

                var applyNightRef = asmDef.MainModule.ImportReference(applyNightMethod);

                var processor = updateMethod.Body.GetILProcessor();
                var instructions = updateMethod.Body.Instructions;
                
                var lastRet = instructions.LastOrDefault(i => i.OpCode == OpCodes.Ret);

                if (lastRet != null)
                {
                    var ldarg0 = Instruction.Create(OpCodes.Ldarg_0);
                    var call = Instruction.Create(OpCodes.Call, applyNightRef);

                    processor.InsertBefore(lastRet, ldarg0);
                    processor.InsertBefore(lastRet, call);
                    Console.WriteLine("Patched Sun.Update successfully!");
                }
                
                asmDef.Write();
            }
        } catch (Exception ex) {
            Console.WriteLine(ex.ToString());
        }
    }
}
