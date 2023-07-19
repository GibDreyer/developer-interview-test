using System;
using System.Diagnostics.Tracing;

namespace Smartwyre.DeveloperTest.Types;

public class CalculateRebateRequest
{
    private decimal volume;

    public CalculateRebateRequest() { }
    public CalculateRebateRequest(string[] args)
    {
        Parse(args);
    }
    public CalculateRebateRequest(string args)
    {
        Parse(args.Split(' ', StringSplitOptions.RemoveEmptyEntries));
    }


    public string? RebateIdentifier { get; set; }
    public string? ProductIdentifier { get; set; }
    public decimal Volume { get => volume; set => volume = value; }

    public bool IsValid() => !string.IsNullOrEmpty(RebateIdentifier)
             && !string.IsNullOrEmpty(RebateIdentifier)
             && volume != default;


    /// <summary>
    /// Used to parse from console line input
    /// </summary>
    /// <param name="args">user supplied input</param>
    private void Parse(string[] args)
    {
        for (int i = 0; i < args.Length; i += 2)
        {
            if (args.Length == i + 1)
                break;

            string arg = args[i].ToLower();
            string value = args[i + 1];

            if (arg == "--rebateid")
                RebateIdentifier = value;
            else if (arg == "--productid")
                ProductIdentifier = value;
            else if (arg == "--volume")
                _ = decimal.TryParse(value, out volume);
            else
                break;
        }
    }
}
