// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

int lineCount = 0;
string filePath = "C:/temp/title.basics.tsv/data.tsv";
foreach (string line in File.ReadLines(filePath).Skip(1))
{
    if (lineCount == 50000)
    {
        break;
    }
    string[] splitLine = line.Split('\t');
    if (splitLine.Length != 9)
    {
        throw new Exception("Ikke rigtigt antal tabs! " + line);
    }
    string tconst = splitLine[0];
    string primaryTitle = splitLine[2];
    string originalTitle = splitLine[3];
    bool isAdult = splitLine[4] == "1";

    lineCount++;
}