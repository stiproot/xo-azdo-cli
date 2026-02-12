namespace Xo.AzDO.Engine.Console;

class Program
{
    /// <summary>
    /// An entry point to call core engine modules.
    /// </summary>
    /// <remarks>
    /// Needs to be extracted into a CLI.
    /// </remarks>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        // System.Console.ReadLine();
        // Core.CreateWorkItems();
        // Core.Folder();
        // Core.Dashboard();
        // Core.QueryByWiql();
        // Core.CloneWorkItem();
        Core.CreateQuery();
        // Core.UpdateWorkItems();
        // Core.UpdateWorkItemHierarchy();
    }
}
