using Isu.Entities;
using Isu.Extra.Entities.AcademicMobility;
using Isu.Extra.Entities.Lessons;
using Isu.Extra.Entities.Lessons.Schedule;
using Isu.Extra.Entities.Megafaculties;
using Isu.Extra.Entities.People;
using Isu.Extra.Entities.Subjects;
using Isu.Extra.Exceptions.Models;
using Isu.Extra.Services;
using Isu.Models;
using Xunit;

namespace Isu.Extra.Test;

public class IsuServiceTest
{
    [Fact]
    public void ScheduleBuild_ExistsInRegisteredStudent()
    {
        var oldService = new Isu.Services.IsuService();
        Group group = oldService.AddGroup(new GroupName("M3102"));
        Student oldStudent = oldService.AddStudent(group, "Nastya Ermolaeva");

        var dayBuilder = new DaySchedule.DayScheduleBuilder();
        dayBuilder.AddLesson(new Lesson
        {
            Subject = new Subject("OOP"),
            Time = new LessonTime(new TimeOnly(13, 30), new TimeOnly(15, 00)),
        });

        var weekBuilder = new Schedule.ScheduleBuilder();
        weekBuilder.AddSchedule(Weeks.EvenWeek, WeekDays.Saturday, dayBuilder.Build());

        var service = new ExtraStudyService(20);
        Schedule schedule = weekBuilder.Build();
        AcademicMobilityStudent newStudent = service.RegisterStudent(oldStudent, schedule);

        Assert.NotNull(newStudent.MainSchedule);
    }

    [Fact]
    public void DayScheduleBuildIntersection_ThrowException()
    {
        var dayBuilder = new DaySchedule.DayScheduleBuilder();

        Action testCode = () =>
        {
            dayBuilder.AddLesson(new Lesson
            {
                Subject = new Subject("OOP"),
                Time = new LessonTime(new TimeOnly(13, 30), new TimeOnly(15, 00)),
            });

            dayBuilder.AddLesson(new Lesson
            {
                Subject = new Subject("OOP"),
                Time = new LessonTime(new TimeOnly(13, 00), new TimeOnly(15, 30)),
            });
        };

        Exception? exception = Record.Exception(testCode);

        Assert.NotNull(exception);
        Assert.IsType<ScheduleException>(exception);
    }

    [Fact]
    public void ExtraService_GetUnregisteredStudents_IsCorrect()
    {
        var oldService = new Isu.Services.IsuService();
        Group group = oldService.AddGroup(new GroupName("M3102"));
        var students = new List<Student>();

        students.Add(oldService.AddStudent(group, "Nastya Ermolaeva"));
        students.Add(oldService.AddStudent(group, "Artyom Zhuikov"));
        students.Add(oldService.AddStudent(group, "Karim Hasan"));
        students.Add(oldService.AddStudent(group, "Andrey Gogolev"));

        var service = new ExtraStudyService(20);

        Assert.True(service.GetUnregisteredStudents(group).Count == 4);
    }

    [Fact]
    public void ExtraService_CheckCreationValidity()
    {
        var service = new ExtraStudyService(20);

        var dayBuilder = new DaySchedule.DayScheduleBuilder();
        dayBuilder.AddLesson(new Lesson
        {
            Subject = new Subject("OOP"),
            Time = new LessonTime(new TimeOnly(13, 30), new TimeOnly(15, 00)),
        });

        dayBuilder.AddLesson(new Lesson
        {
            Subject = new Subject("OOP"),
            Time = new LessonTime(new TimeOnly(15, 30), new TimeOnly(17, 00)),
        });

        var weekBuilder = new Schedule.ScheduleBuilder();
        weekBuilder.AddSchedule(Weeks.EvenWeek, WeekDays.Saturday, dayBuilder.Build());

        Megafaculty megafaculty = service.CreateMegafaculty("Tint");
        Subject course = service.RegisterExtraStudyCourse(megafaculty, "OOP - extra");
        AcademicMobilityStream mobilityStream = service.CreateAcademicMobilityStream(megafaculty, course);
        AcademicMobilityGroup newGroup = service.CreateAcademicMobilityGroup(mobilityStream, "OOP-1/4", weekBuilder.Build());

        Assert.True(mobilityStream.CapacityTotal > 0);
        Assert.True(newGroup.Capacity > 0);
        Assert.True(megafaculty.HasCourse(course));
    }

    [Fact]
    public void MegaFacultyParser_IsCorrect()
    {
        var oldService = new Isu.Services.IsuService();
        Group group = oldService.AddGroup(new GroupName("M3102"));
        Student oldStudent = oldService.AddStudent(group, "Nastya Ermolaeva");

        Assert.Equal("MegafacultyOfTranslationalTechnologies", MegafacultyParser.Parse(oldStudent.Group.GroupName).Name);

        Group group2 = oldService.AddGroup(new GroupName("P3102"));
        Student oldStudent2 = oldService.AddStudent(group2, "Artyom Zhuikov");

        Assert.Equal("MegafacultyOfComputerTechnologiesAndControl", MegafacultyParser.Parse(oldStudent2.Group.GroupName).Name);

        Group group3 = oldService.AddGroup(new GroupName("U3102"));
        Student oldStudent3 = oldService.AddStudent(group3, "George Kruglov");

        Assert.Equal("FacultyOfTechnologicalManagementAndInnovation", MegafacultyParser.Parse(oldStudent3.Group.GroupName).Name);
    }

    [Fact]
    public void MobilityStream_GetCoursesAndStreams_Correct()
    {
        var faculty = new Megafaculty("Tint");

        faculty.AppendExtraStudyCourse(new Subject("OOP"));
        faculty.AppendExtraStudyCourse(new Subject("C++"));
        faculty.AppendExtraStudyCourse(new Subject("Java"));

        Assert.True(faculty.GetExtraStudyCourses().Count == 3);
        Assert.True(faculty.GetAllExtraStudyStreams().Count == 0);
    }
}