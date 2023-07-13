namespace Isu.Models;

public static class EducationalProgramParser
{
    public static EducationalProgram Parse(string groupName)
    {
        // коды образовательных программ по ОКСО
        switch (groupName[1])
        {
            case '2':
                return EducationalProgram.Spo;
            case '3':
                return EducationalProgram.Bachelor;
            case '4':
                return EducationalProgram.Masters;
            case '5':
                return EducationalProgram.Specialist;
            case '6':
                return EducationalProgram.PhD;
            case '7':
                return EducationalProgram.PostDoc;
            default:
                return EducationalProgram.Undefined;
        }
    }
}