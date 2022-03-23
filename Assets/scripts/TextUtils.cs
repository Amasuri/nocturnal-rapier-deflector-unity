using System;

static public class TextUtils
{
    /// <summary>
    /// Get a line, which is word-splitted to fit into certain X length.
    /// </summary>
    static public string GetFittedLine(string original, int maxXCharLength)
    {
        var splitString = original.Split(' ');
        var resultingLine = "";
        var currLineLength = 0;
        foreach (var word in splitString)
        {
            if (currLineLength + word.Length <= maxXCharLength)
            {
                resultingLine += word + " ";
                currLineLength += word.Length + 1;
            }
            else
            {
                currLineLength = 0;
                resultingLine += "\n" + word + " ";
                currLineLength += word.Length + 1;
            }
        }

        return resultingLine;
    }

    /// <summary>
    /// Get a line, which is word-splitted to fit into certain X length and depe
    /// </summary>
    static public string GetTimeWordSplitFittedLine(string original, int maxLength, int msSinceBeginning)
    {
        var splitString = original.Split(' ');

        const int newWordAfterThisMs = 100;
        int wordsNow = msSinceBeginning / newWordAfterThisMs;

        if (wordsNow > splitString.Length)
            wordsNow = splitString.Length;

        string newStr = "";
        for (int i = 0; i < wordsNow; i++)
        {
            newStr += splitString[i] + " ";
        }

        return GetFittedLine(newStr, maxLength);
    }

    /// <summary>
    /// Get a line, which is char-splitted to fit into certain X length and depe
    /// </summary>
    static public string GetTimeCharSplitFittedLine(string original, int maxLength, int msSinceBeginning, char actor = 'n')
    {
        var splitString = original.ToCharArray();

        const int newCharAfterThisMs = 25;
        int charsNow = msSinceBeginning / newCharAfterThisMs;

        if (charsNow > splitString.Length)
            charsNow = splitString.Length;

        string newStr = "";
        for (int i = 0; i < charsNow; i++)
        {
            newStr += splitString[i];
        }

        if (charsNow < splitString.Length)
            FireVoicedEvent(actor);

        return GetFittedLine(newStr, maxLength);
    }

    static public bool HasFinishedPhrase(string original, int maxLength, int msSinceBeginning)
    {
        var splitString = original.ToCharArray();

        const int newCharAfterThisMs = 25;
        int charsNow = msSinceBeginning / newCharAfterThisMs;

        if (charsNow < splitString.Length)
            return false;

        return true;
    }

    private static void FireVoicedEvent(char actor)
    {
        //var args = new VoicePlayer.VoicedTextUpdatedEventArgs
        //{
        //    delta = WitcheryGame.DeltaUpdate,
        //    pace = VoicePlayer.maxMsSinceLastBitPaceNormal,
        //    actor = actor,
        //};

        //VoicePlayer.OnVoicedText(args);
    }
}
