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

            // Always start from a clean backup to avoid multiple injections if re-run
            if (System.IO.File.Exists(backupPath)) {
                System.IO.File.Copy(backupPath, assemblyPath, true);
            } else {
                System.IO.File.Copy(assemblyPath, backupPath);
            }

            var resolver = new DefaultAssemblyResolver();
            resolver.AddSearchDirectory(managedDir);

            var parameters = new ReaderParameters { AssemblyResolver = resolver, ReadWrite = true };
            using (var asmDef = AssemblyDefinition.ReadAssembly(assemblyPath, parameters))
            {
                // Inject into TimeManager.Awake
                var targetType = asmDef.MainModule.Types.FirstOrDefault(t => t.FullName == "ProjectAutomata.TimeManager");
                if (targetType == null) {
                    Console.WriteLine("Could not find TimeManager type");
                    return;
                }

                var targetMethod = targetType.Methods.FirstOrDefault(m => m.Name == "Awake");
                if (targetMethod == null) {
                    Console.WriteLine("Could not find Awake method");
                    return;
                }

                var hookAsmDef = AssemblyDefinition.ReadAssembly(managedDir + @"\AlwaysNightHook.dll", parameters);
                var hookType = hookAsmDef.MainModule.Types.FirstOrDefault(t => t.FullName == "AlwaysNightHook");
                var initMethod = hookType.Methods.FirstOrDefault(m => m.Name == "Init");

                var initRef = asmDef.MainModule.ImportReference(initMethod);

                var processor = targetMethod.Body.GetILProcessor();
                
                // Insert Call to Init at the very beginning
                var firstInstruction = targetMethod.Body.Instructions[0];
                var call = Instruction.Create(OpCodes.Call, initRef);
                processor.InsertBefore(firstInstruction, call);

                Console.WriteLine("Patched TimeManager.Awake successfully!");
                asmDef.Write();
            }
        } catch (Exception ex) {
            Console.WriteLine(ex.ToString());
        }
    }
}
