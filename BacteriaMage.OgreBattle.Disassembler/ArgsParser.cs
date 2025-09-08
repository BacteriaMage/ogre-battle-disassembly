// github.com/BacteriaMage

using BacteriaMage.OgreBattle.Disassembler.Exceptions;
using BacteriaMage.OgreBattle.Disassembler.Extensions;

namespace BacteriaMage.OgreBattle.Disassembler;

public static class ArgsParser
{
    public abstract class ParserOption(string name)
    {
        public readonly string Name = name;
        
        public bool Match(string? parameter)
        {
            return string.Equals(parameter, Name, StringComparison.OrdinalIgnoreCase);
        }
    };

    public static ParserOption Required(string name, Action<string> action)
    {
        return new RequiredOption(name, action);
    }

    public static ParserOption Optional(string name, Action<string> action)
    {
        return new OptionalOption(name, action);   
    }

    public static ParserOption Switch(string name, Action action)
    {
        return new SwitchOption(name, action);
    }

    public static void ParseArgs(string[] args, IEnumerable<ParserOption> options)
    {
        var optionsList = options.ToList();
        
        var arguments = optionsList.Where(option => option is not SwitchOption).ToList();
        var switches = optionsList.Where(option => option is SwitchOption).ToList();

        foreach (string parameter in args)
        {
            if (TryParseSwitch(parameter, out string name))
            {
                ProcessSwitch(switches, name);
            }
            else
            {
                ProcessArgument(arguments, parameter);
            }
        }
        
        ProcessUnsatisfiedArgument(arguments);
    }
    
    private static bool TryParseSwitch(string parameter, out string name)
    {
        if (parameter.StartsWith("--") && parameter.Length > 2)
        {
            name = parameter[2..];
            return true;
        }
        else if((parameter.StartsWith('-') || parameter.StartsWith('/')) && parameter.Length > 1)
        {
            name = parameter[1..];
            return true;
        }
        else
        {
            name = string.Empty;
            return false;
        }
    }

    private static void ProcessSwitch(List<ParserOption> switches, string name)
    {
        foreach (var option in switches)
        {
            if (option is SwitchOption switchOption)
            {
                if (switchOption.Match(name))
                {
                    switchOption.Action();
                    return;
                }
            }
        }
        
        throw new ArgumentException($"Unknown switch: {name}");   
    }

    private static void ProcessArgument(List<ParserOption> arguments, string value)
    {
        if (arguments.IsNotEmpty())
        {
            var argument = (ArgumentOption)arguments.Dequeue();
            argument.Action(value);
        }
        else
        {
            throw new ParameterException($"Unexpected parameter: {value}");
        }
    }

    private static void ProcessUnsatisfiedArgument(List<ParserOption> arguments)
    {
        if (arguments.Any(option => option is RequiredOption))
        {
            var argument = (ArgumentOption)arguments.Dequeue();
            throw new ParameterException($"Expected parameter: {argument.Name}");
        }
    }

    private abstract class ArgumentOption(string name, Action<string> action) : ParserOption(name)
    {
        public readonly Action<string> Action = action;
    }

    private sealed class RequiredOption(string name, Action<string> action) : ArgumentOption(name, action);
    
    private sealed class OptionalOption(string name, Action<string> action) : ArgumentOption(name, action);
    
    private sealed class SwitchOption(string name, Action action) : ParserOption(name)
    {
        public readonly Action Action = action;
    }
}