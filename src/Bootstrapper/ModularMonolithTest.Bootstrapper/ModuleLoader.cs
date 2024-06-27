using ModularMonolithTest.Shared.Abstractions.Modules;
using System.Reflection;

namespace ModularMonolithTest.Bootstrapper
{
    internal static class ModuleLoader
    {
        public static IList<Assembly> LoadAssemblies(IConfiguration configuration)
        {
            const string modulePart = "ModularMonolithTest.Modules.";
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var locations = assemblies.Where(x => !x.IsDynamic).Select(x => x.Location).ToArray();
            //var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
            //    .Where(x => !locations.Contains(x, StringComparer.InvariantCultureIgnoreCase))
            //    .ToList();
            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Where(x => x.Contains(modulePart))
                .ToList();
            //foreach(var location in locations)
            //{

            //    Console.WriteLine(location);
            //}
            //Console.WriteLine("---------------------------------------------------------------------------");
            //foreach (var file in files)
            //{

            //    Console.WriteLine(file);
            //}
            //Console.WriteLine("---------------------------------------------------------------------------");

            //var disabledModules = new List<string>();
            //foreach (var file in files)
            //{
            //    if (!file.Contains(modulePart))
            //    {
            //        continue;
            //    }
            //    Console.WriteLine(file);
            //    var moduleName = file.Split(modulePart)[1].Split(".")[0].ToLowerInvariant();
            //    var enabled = configuration.GetValue<bool>($"{moduleName}:module:enabled");
            //    if (!enabled)
            //    {
            //        disabledModules.Add(file);
            //    }
            //}

            //foreach (var disabledModule in disabledModules)
            //{
            //    files.Remove(disabledModule);
            //}

            files.ForEach(x => assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(x))));
            //foreach(var assembly in assemblies)
            //{
            //    Console.WriteLine(assembly.FullName);
            //}
            return assemblies;
            
        }

        public static IList<IModule> LoadModules(IEnumerable<Assembly> assemblies)
            => assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(IModule).IsAssignableFrom(x) && !x.IsInterface)
                .OrderBy(x => x.Name)
                .Select(Activator.CreateInstance)
                .Cast<IModule>()
                .ToList();
    }
}
