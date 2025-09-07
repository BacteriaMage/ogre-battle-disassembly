// github.com/BacteriaMage

namespace BacteriaMage.OgreBattle.Disassembler;

public class Program
{
    private void ParseArgs(string[] args)
    {
        Messenger.ShowVerbose = !args.Contains("-q");
    }

    private void Run()
    {
    }

    private void TryMain(string[] args)
    {
        ParseArgs(args);
        Run();
    }
    
    public static int Main(string[] args)
    {
        try
        {
            new Program().TryMain(args);
            return 0;
        }
        catch (Exception e)
        {
            Messenger.Error(e.Message);
            Messenger.Verbose(e.StackTrace);
            return 1;
        }
    }
}