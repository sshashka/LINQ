using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Rozklad.V2.Controllers;
using Rozklad.V2.DataAccess;
using Rozklad.V2.Entities;
using Rozklad.V2.Models;
using Rozklad.V2.Services;
using Xunit;

namespace Rozkald.V2.Tests
{
    public class SubjectControllerTest
    {
        [Fact]
        public async void CanSetLinks()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Rozklad")
                .Options;
            using(var context = new ApplicationDbContext(options))
            {
                var studentId = Guid.NewGuid();
                var subjectId = Guid.NewGuid();
                var groupId = Guid.NewGuid();
                await context.Students.AddAsync(new Student
                {
                    Id = studentId,
                    GroupId= groupId
                });
                await context.Subjects.AddAsync(new Subject
                {
                    Id = subjectId,
                    Name = "Some subject",
                    GroupId = groupId
                });
                await context.SaveChangesAsync();
            
                var mapperMock = new Mock<IMapper>();
                var repository = new RozkladRepository(context);

                var controller = new SubjectsForStudentController(mapperMock.Object, repository);

                var sujectLinks = new PostLinksDto
                {
                    LabsLink = "aaaaaa",
                    LessonsLink = "bbbbbb"
                };
                // Act 
                var subjectLinksResponse = await controller.SetLinks(studentId, subjectId ,sujectLinks);
                // Assert 
                var linksResponse = (SubjectLinksForStudent)((OkObjectResult) subjectLinksResponse).Value;
                var linksCreated = context.SubjectLinksForStudent.FirstOrDefault();
                Assert.NotNull(linksCreated);
                Assert.Equal(linksCreated.StudentId , linksResponse.StudentId);
                Assert.Equal(linksCreated.SubjectId , linksResponse.SubjectId);
                Assert.Equal("aaaaaa", linksCreated.LabsLink );
                Assert.Equal("bbbbbb",  linksCreated.LabsLink );
            }
        }
    }
}