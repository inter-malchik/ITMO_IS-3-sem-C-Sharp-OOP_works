using Isu.Extra.Exceptions.Models;
using Isu.Models;

namespace Isu.Extra.Entities.Megafaculties;

public static class MegafacultyParser
{
    public static Megafaculty Parse(string groupName)
    {
        return Parse(groupName[0]);
    }

    public static Megafaculty Parse(GroupName groupName)
    {
        return Parse(groupName.FacultyLetter);
    }

    public static Megafaculty Parse(char facultyLetter)
    {
        switch (facultyLetter)
        {
            case 'M':
            case 'K':
            case 'J':
                return new Megafaculty("MegafacultyOfTranslationalTechnologies");
            case 'R':
            case 'P':
            case 'N':
            case 'H':
                return new Megafaculty("MegafacultyOfComputerTechnologiesAndControl");
            case 'U':
                return new Megafaculty("FacultyOfTechnologicalManagementAndInnovation");
            case 'G':
            case 'T':
            case 'O':
                return new Megafaculty("MegafacultyOfBioTechnologies");
            case 'L':
            case 'Z':
            case 'W':
                return new Megafaculty("PhysTechMegafaculty");
            default:
                throw MegafacultyException.ParsingException(facultyLetter);
        }
    }
}