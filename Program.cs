using System.IO;
using System.Linq;
using Solidoc.Serializers;
using Solidoc.Utility;

namespace Solidoc
{
    public class Program
    {
        /***********************************************************************************************
            Arguments:
            1. Path to truffle project root.
            2. Path to generate documentation to.
            3. Do not recompile.
        *************************************************************************************************/
        internal static void Main(string[] args)
        {
            ResourceWriter.Run();

            if (args.Length > 3)
            {
                ConsoleUtility.WriteException(string.Format(I18N.InvalidCommand, string.Join(" ", args)));
                return;
            }

            string pathToRoot = args[0];
            string outputPath = args[1];
            bool noCompilation = (args.Length == 3 ? args[2] : "").ToLower().StartsWith("t");

            if(!Directory.Exists(pathToRoot))
            {
                ConsoleUtility.WriteException(string.Format(I18N.InvalidDirectory, pathToRoot));
                return;
            }

            if(!noCompilation)
            {
                Compile(pathToRoot);
            }

            string pathToBuildDirectory = Path.Combine(pathToRoot, "build", "contracts");

            var directory = new DirectoryInfo(outputPath);

            if (!directory.Exists)
            {
                directory.Create();
            }

            var parser = new ContractParser(pathToBuildDirectory);
            var contracts = parser.Parse();

            var generator = new Serializer(contracts.ToList(), outputPath);
            generator.Serialize();
        }

        private static void Compile(string pathToRoot)
        {
            if (Directory.Exists(Path.Combine(pathToRoot, "build")))
            {
                Directory.Delete(Path.Combine(pathToRoot, "build"), true);
            }

            TruffleCompiler.Compile(pathToRoot);
        }
    }
}