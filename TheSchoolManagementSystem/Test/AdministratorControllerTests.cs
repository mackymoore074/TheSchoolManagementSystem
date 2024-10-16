using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using TheSchoolManagementSystem.Controllers;
using TheSchoolManagementSystem.Data;
using TheSchoolManagementSystem.Models;
using Xunit;

namespace TheSchoolManagementSystem.Tests
{
    public class AdministratorControllerTests
    {
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly Mock<DbSet<Student>> _mockStudentSet;
        private readonly AdministratorController _controller;

        public AdministratorControllerTests()
        {
            _mockContext = new Mock<ApplicationDbContext>();
            _mockStudentSet = new Mock<DbSet<Student>>();
            _mockContext.Setup(m => m.Students).Returns(_mockStudentSet.Object);
            _controller = new AdministratorController(_mockContext.Object);
        }

        [Fact]
        public void Index_ReturnsViewResult()
        {
            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult);
        }

        [Fact]
        public async Task Students_ReturnsViewWithListOfStudents()
        {
            // Arrange
            var students = new List<Student>
            {
                new Student { StudentId = 1, FirstName = "John", LastName = "Doe" },
                new Student { StudentId = 2, FirstName = "Jane", LastName = "Doe" }
            };

            _mockStudentSet.As<IQueryable<Student>>().Setup(m => m.Provider).Returns(students.AsQueryable().Provider);
            _mockStudentSet.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(students.AsQueryable().Expression);
            _mockStudentSet.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(students.AsQueryable().ElementType);
            _mockStudentSet.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(students.GetEnumerator());

            _mockStudentSet.Setup(m => m.ToListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(students);

            // Act
            var result = await _controller.Students();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Student>>(viewResult.Model);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public async Task CreateStudent_ValidModel_RedirectsToStudents()
        {
            // Arrange
            var student = new Student { StudentId = 1, FirstName = "John", LastName = "Doe" };

            // Act
            var result = await _controller.CreateStudent(student);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Students", redirectResult.ActionName);
            _mockStudentSet.Verify(m => m.Add(It.IsAny<Student>()), Times.Once);
            _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteStudentConfirmed_StudentExists_RedirectsToStudents()
        {
            // Arrange
            var student = new Student { StudentId = 1, FirstName = "John", LastName = "Doe" };

            _mockStudentSet.Setup(m => m.FindAsync(student.StudentId)).ReturnsAsync(student);

            // Act
            var result = await _controller.DeleteStudentConfirmed(student);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Students", redirectResult.ActionName);
            _mockStudentSet.Verify(m => m.Remove(student), Times.Once);
            _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
