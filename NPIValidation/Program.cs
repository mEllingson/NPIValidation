// See https://aka.ms/new-console-template for more information
using NPIValidation;

// Create a list of NPIs to test    
var testNpiList = new List<string>()
{
    "808401234567893",
    "1234567893",
    "1013951748",
    "808401234567892", //Should be invalid
    "1234567892", //Should be invalid
};

var results = testNpiList.Select(npi => new { Npi = npi, IsValid = NpiUtility.Validate(npi) });

foreach(var result in results)
{
    Console.WriteLine($"NPI: {result.Npi} is valid: {result.IsValid}");
}

Console.ReadLine();