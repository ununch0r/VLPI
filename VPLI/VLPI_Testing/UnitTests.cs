using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Business.Managers;
using DataAccess;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Vlpi.Web.Controllers;
using Vlpi.Web.Mapper;
using Vlpi.Web.ViewModels.TaskViewModels;
using System.Data.Entity.Infrastructure;
using MockQueryable.Moq;

namespace VLPI_Testing
{
    public class Tests
    {
        private static IMapper _mapper;
        private Mock<VLPIContext> databaseContextMockup;

        [SetUp]
        public void Setup()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new AutoMapping()); });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;

            }

            databaseContextMockup = GetMock();
        }

        public Mock<VLPIContext> GetMock()
        {
            var data = new List<Core.Entities.Task>
            {
                new Core.Entities.Task()
                {
                    Id = 1,
                    Complexity = 1,
                    Description = "Description",
                    Objective = "Objective",
                    PhotoUrl = "url",
                    StandardAnswer = "Answer",
                    TypeId = 1
                }
            }.AsQueryable();
            //var mockSet = new Mock<DbSet<Core.Entities.Task>>();
            //mockSet.As<IDbAsyncEnumerable<Core.Entities.Task>>()
            //    .Setup(m => m.GetAsyncEnumerator())
            //    .Returns(new TestDbAsyncEnumerator<Core.Entities.Task>(data.GetEnumerator()));

            //mockSet.As<IQueryable<Core.Entities.Task>>()
            //    .Setup(m => m.Provider)
            //    .Returns(new TestDbAsyncQueryProvider<Core.Entities.Task>(data.Provider));

            //mockSet.As<IQueryable<Core.Entities.Task>>().Setup(m => m.Expression).Returns(data.Expression);
            //mockSet.As<IQueryable<Core.Entities.Task>>().Setup(m => m.ElementType).Returns(data.ElementType);
            //mockSet.As<IQueryable<Core.Entities.Task>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            //var mockContext = new Mock<VLPIContext>();
            //mockContext.Setup(c => c.Task).Returns(mockSet.Object);
            //return mockContext;
            var mock = data.AsQueryable().BuildMockDbSet();
            var mockContext = new Mock<VLPIContext>();
            mockContext.Setup(c => c.Task).Returns(mock.Object);
            return mockContext;
        }

        [Test]
        public async Task TaskPostTest()
        {
            TaskController controller =
                new TaskController(
                    new TaskManager(new TaskRepository(databaseContextMockup.Object), _mapper,
                        new RequirementManager(new RequirementRepository(databaseContextMockup.Object))), _mapper);
            CreateTaskViewModel newTask = new CreateTaskViewModel();
            newTask.Complexity = 1;
            newTask.Description = "Some description";
            newTask.Objective = "objective";
            newTask.StandardAnswer = "standart answer";
            newTask.TypeId = 1;

            var result = await controller.CreateTaskAsync(newTask);
            Assert.True(result is OkObjectResult);
        }

        [Test]
        public async Task TaskManagerTest()
        {
            var manager = GetTaskManager();
            var expectedresult = new Core.Entities.Task()
            {
                Id = 1,
                Complexity = 1,
                Description = "Description"
            };

            var result = await manager.GetAsync(1);
            bool equal = CompareTasks(expectedresult, result);
            Assert.True(equal);
        }

        public TaskManager GetTaskManager()
        {
            return new TaskManager(new TaskRepository(databaseContextMockup.Object), _mapper,
                new RequirementManager(new RequirementRepository(databaseContextMockup.Object)));
        }

        private bool CompareTasks(Core.Entities.Task task1, Core.Entities.Task task2)
        {
            return task1.Id == task2.Id && task1.Complexity == task2.Complexity &&
                   task1.Description == task2.Description;
        }
    }
}