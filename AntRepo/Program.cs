using AntRepo;


string[] gelenVagonlar = new[]
{
    "10808313931813319430761116496",
    "93876532983858416774152932536"
};
string depoDurum = "0#54134427902231984111412732221";

string result = AntrepoManagement.AntrepoYerlestir(depoDurum,gelenVagonlar);

Console.WriteLine(result);