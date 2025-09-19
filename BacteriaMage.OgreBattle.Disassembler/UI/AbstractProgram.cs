// github.com/BacteriaMage

namespace BacteriaMage.OgreBattle.Disassembler.UI;

/// <summary>
/// Base class for program entry points.
/// </summary>
public abstract class AbstractProgram
{
    /// <summary>
    /// Main execution method for the program.
    /// </summary>
    protected abstract void Main();
    
    /// <summary>
    /// Optional handler for command-line arguments.
    /// </summary>
    protected virtual void ParseArgs(string[] args)
    {
    }

    /// <summary>
    /// Runs the program.
    /// </summary>
    public int Run(string[] args)
    {
        try
        {
            ParseArgs(args);
            Main();
            return 0;
        }
        catch (Exception e)
        {
            MessageDisplay.Error(e.Message);
            MessageDisplay.Verbose(e.StackTrace);
            return 1;
        }
    }
}