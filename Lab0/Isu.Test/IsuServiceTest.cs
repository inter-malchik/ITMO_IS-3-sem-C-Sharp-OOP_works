using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Xunit;

namespace Isu.Test;

public class IsuServiceTest
{
    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        var service = new Services.IsuService();
        Group group = service.AddGroup(new GroupName("M3102"));
        Student student = service.AddStudent(group, "Nastya Ermolaeva");

        Assert.True(student.Group.GroupName.FullName == group.GroupName.FullName, "Student should have group");
        Assert.True(group.Students.Contains(student), "Group should have student");
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        var service = new Services.IsuService(1);
        Group group = service.AddGroup(new GroupName("M3102"));

        Action testCode = () =>
        {
            service.AddStudent(group, "Nastya Ermolaeva");
            service.AddStudent(group, "Artyom Zhuikov");
        };

        Exception exception = Record.Exception(testCode);

        Assert.NotNull(exception);
        Assert.IsType<MaxStudentsPerGroupException>(exception);
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        var service = new Services.IsuService();

        Action testCode = () =>
        {
            service.AddGroup(new GroupName("CUM3102"));
        };

        Exception exception = Record.Exception(testCode);

        Assert.NotNull(exception);
        Assert.IsType<InvalidGroupNameException>(exception);
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        var service = new Services.IsuService();
        Group oldGroup = service.AddGroup(new GroupName("M3102"));
        Group newGroup = service.AddGroup(new GroupName("M3112"));

        Student student = service.AddStudent(oldGroup, "Nastya Ermolaeva");
        service.ChangeStudentGroup(student, newGroup);

        Assert.True(student.Group.ToString() == newGroup.ToString(), "Group should have been changed");
    }
}