namespace Nzl.Util
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Util for loading assembly to get types and objects.
    /// </summary>
    public static class AssemblyUtil
    {
        /// <summary>
        /// Get implementd types by directory.
        /// </summary>
        /// <typeparam name="T">The generic type.</typeparam>
        /// <param name="baseDirectory">The directory including the assemlbies.</param>
        /// <returns>The type list.</returns>
        public static IEnumerable<Type> GetImplementdTypesByDirectory<T>(string baseDirectory)
        {
            IList<Assembly> assemblies = GetAssemblies(baseDirectory);
            List<Type> types = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                types.AddRange(GetImplementdTypes<T>(assembly));
            }

            return types;
        }

        /// <summary>
        /// Get implementd types by assembly file.
        /// </summary>
        /// <typeparam name="T">The generic type.</typeparam>
        /// <param name="assemblyFile">The assembly file name.</param>
        /// <returns>The type list.</returns>
        public static IEnumerable<Type> GetImplementdTypes<T>(string assemblyFile)
        {
            if (!File.Exists(assemblyFile))
            {
                return null;
            }

            try
            {
                return GetImplementdTypes<T>(Assembly.LoadFile(assemblyFile));
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get implementd types by assembly.
        /// </summary>
        /// <typeparam name="T">The generic type.</typeparam>
        /// <param name="assembly">The assembly.</param>
        /// <returns>The type list.</returns>
        public static IEnumerable<Type> GetImplementdTypes<T>(Assembly assembly)
        {
            if (assembly == null)
            {
                return null;
            }

            return assembly.GetExportedTypes().Where(p => (p.IsSubclassOf(typeof(T)) || typeof(T).IsAssignableFrom(p)) && (!p.IsAbstract) && (!p.IsInterface));
        }

        /// <summary>
        /// Get implemented objects by directory.
        /// </summary>
        /// <typeparam name="T">The generic type.</typeparam>
        /// <param name="baseDirectory">The directory including the assemlbies.</param>
        /// <returns>The generic type list.</returns>
        public static IList<T> GetImplementedObjectsByDirectory<T>(string baseDirectory)
        {
            IList<Assembly> assemblies = GetAssemblies(baseDirectory);
            List<T> entities = new List<T>();
            foreach (Assembly assembly in assemblies)
            {
                entities.AddRange(GetImplementedObjects<T>(assembly));
            }

            return entities;
        }

        /// <summary>
        /// Get implemented objects by assemlby file.
        /// </summary>
        /// <typeparam name="T">The generic type.</typeparam>
        /// <param name="assemblyFile">The assembly file name.</param>
        /// <returns>The generic type list.</returns>
        public static IList<T> GetImplementedObjects<T>(string assemblyFile)
        {
            if (!File.Exists(assemblyFile))
            {
                return null;
            }

            try
            {
                return GetImplementedObjects<T>(Assembly.LoadFile(assemblyFile));
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get implementd objects by assembly.
        /// </summary>
        /// <typeparam name="T">The generic type.</typeparam>
        /// <param name="assembly">The assembly.</param>
        /// <returns>The generic type list.</returns>
        public static IList<T> GetImplementedObjects<T>(Assembly assembly)
        {
            if (assembly == null)
            {
                return null;
            }

            IEnumerable<Type> types = GetImplementdTypes<T>(assembly);
            var result = new List<T>();
            foreach (Type type in types)
            {
                ConstructorInfo constructor = type.GetConstructor(new Type[0]);
                if (constructor == null)
                {
                    continue;
                }

                object instance = Activator.CreateInstance(type);
                if (instance is T)
                {
                    result.Add((T)instance);
                }
            }

            return result;
        }

        /// <summary>
        /// Get assemblies under the target directory.
        /// </summary>
        /// <param name="baseDirectory">The directory including the assemlies.</param>
        /// <returns>The assembly list.</returns>
        public static IList<Assembly> GetAssemblies(string baseDirectory)
        {
            if (!Directory.Exists(baseDirectory))
            {
                return new List<Assembly>();
            }

            return GetAssemblies(Directory.GetFiles(baseDirectory, "*.dll"));
        }

        /// <summary>
        /// Get assemblies by assembly files.
        /// </summary>
        /// <param name="assemblyFiles">The assembly files.</param>
        /// <returns>The assembly list.</returns>
        public static IList<Assembly> GetAssemblies(string[] assemblyFiles)
        {
            IList<Assembly> assemblies = new List<Assembly>();
            try
            {
                foreach (string file in assemblyFiles)
                {
                    if (!File.Exists(file) || (!file.EndsWith(".dll", StringComparison.InvariantCultureIgnoreCase)))
                    {
                        continue;
                    }

                    assemblies.Add(Assembly.LoadFile(file));
                }
            }
            catch
            {
                return new List<Assembly>();
            }

            return assemblies;
        }
    }
}
