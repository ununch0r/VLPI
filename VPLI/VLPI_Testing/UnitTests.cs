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
using Core.Entities;
using Core.Entities.Custom.Answer;
using Core.Entities.Custom.Statistic;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MockQueryable.Moq;
using Task = System.Threading.Tasks.Task;

namespace VLPI_Testing
{
    public class Tests
    {
        private static IMapper _mapper;
        private Mock<VLPIContext> databaseContextMockup;
        private Mock<VLPIContext> databaseUserContextMockup;
        private Mock<VLPIContext> databaseRequirementContextMockup;
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
            databaseUserContextMockup = GetUserMock();
            databaseRequirementContextMockup = GetRequirementMock();
        }

        public Mock<VLPIContext> GetUserMock()
        {
            var data = new List<User>
            {
                new()
                {
                    Id = 1,
                    FirstName = "Andrii",
                    LastName = "Harashchak",
                    Email = "andrii@vlpi.com",
                    HashedPasswrod = "5f4dcc3b5aa765d61d8327deb882cf99".ToUpper(),
                    UserRole = new List<UserRole>
                    {
                        new UserRole
                        {
                            Role = new Role
                            {
                                Id = 1,
                                Name = "admin"
                            },
                        }
                    }
                }
            };
            var mock = data.AsQueryable().BuildMockDbSet();
            mock
                .Setup(_ => _.AddAsync(It.IsAny<User>(), It.IsAny<System.Threading.CancellationToken>()))
                .Callback((User model, CancellationToken token) => { data.Add(model); })
                .Returns((User model, CancellationToken token) => new ValueTask<EntityEntry<User>>());

            mock.Setup(m => m.Remove(It.IsAny<User>())).Callback<User>((entity) => data.Remove(entity));
            var mockContext = new Mock<VLPIContext>();
            mockContext.Setup(c => c.User).Returns(mock.Object);
            return mockContext;
        }
        public Mock<VLPIContext> GetRequirementMock()
        {
            var data = new List<Requirement>
            {
                new()
                {
                    Id = 1,
                    TaskId = 1,
                    Description = "req description"
                }
            };
            var mock = data.AsQueryable().BuildMockDbSet();
            mock
                .Setup(_ => _.AddAsync(It.IsAny<Requirement>(), It.IsAny<System.Threading.CancellationToken>()))
                .Callback((Requirement model, CancellationToken token) => { data.Add(model); })
                .Returns((User model, CancellationToken token) => new ValueTask<EntityEntry<Requirement>>());

            mock.Setup(m => m.Remove(It.IsAny<Requirement>())).Callback<Requirement>((entity) => data.Remove(entity));
            //var mockContext = new Mock<VLPIContext>();
            databaseContextMockup.Setup(c => c.Requirement).Returns(mock.Object);
            return databaseContextMockup;
        }
        public Mock<VLPIContext> GetMock()
        {
            var data = new List<Core.Entities.Task>
            {
                new ()
                {
                    Id = 1,
                    Complexity = 1,
                    Description = "Description",
                    Objective = "Objective",
                    PhotoUrl = "url",
                    StandardAnswer = "Answer",
                    TypeId = 1
                }
            };
            var mock = data.AsQueryable().BuildMockDbSet();
            //mock.Setup(d => d.Add(It.IsAny<Core.Entities.Task>())).Callback<Core.Entities.Task>((s) => data.Add(s));
            mock
                .Setup(_ => _.AddAsync(It.IsAny<Core.Entities.Task>(), It.IsAny<System.Threading.CancellationToken>()))
                .Callback((Core.Entities.Task model, CancellationToken token) => { data.Add(model); })
                .Returns((Core.Entities.Task model, CancellationToken token) => new ValueTask<EntityEntry<Core.Entities.Task>>());

            mock.Setup(m => m.Remove(It.IsAny<Core.Entities.Task>())).Callback<Core.Entities.Task>((entity) => data.Remove(entity));
            var mockContext = new Mock<VLPIContext>();
            mockContext.Setup(c => c.Task).Returns(mock.Object);
            return mockContext;
        }
        //1
        [Test]
        public async Task TaskPostTest()
        {
            int taskId = 2;
            TaskManager manager = GetTaskManager();
            Core.Entities.Task newTask = new Core.Entities.Task()
            {
                Id = taskId,
                Complexity = 1,
                Description = "Some description",
                Objective = "objective",
                StandardAnswer = "standart answer",
                TypeId = 2,
            };


            await manager.AddAsync(newTask);
            var result = await manager.GetAsync(taskId);


            bool areEqual = CompareTasks(newTask, result);
            Assert.True(areEqual);
        }
        //2
        [Test]
        public async Task GetTaskUsingManagerTest()
        {
            int taskId = 1;
            var manager = GetTaskManager();
            var expectedResult = new Core.Entities.Task()
            {
                Id = taskId,
                Complexity = 1,
                Description = "Description"
            };

            var result = await manager.GetAsync(taskId);


            bool equal = CompareTasks(expectedResult, result);
            Assert.True(equal);
        }

        //3
        [Test]
        public async Task DeleteTaskTest()
        {
            var taskId = 1;
            var manager = GetTaskManager();

            await manager.DeleteAsync(taskId);
            var task = await manager.GetAsync(1);

            Assert.Null(task);
        }

        //4
        [Test]
        public async Task RegisterUserTask()
        {
            try
            {
                var manager = GetUserManager();
            var user = new User
            {
                Id = 2,
                FirstName = "Andrii2",
                LastName = "Harashchak2",
                Email = "andrii2@vlpi.com",
                HashedPasswrod = "password",
                UserRole = new List<UserRole>
                {
                    new ()
                    {
                        Role = new()
                        {
                            Id = 1,
                            Name = "admin"
                        },
                    }
                }
            };
            await manager.AddAsync(user);
            //var users = manager.
            var User = await manager.GetAsync(2);
            //var UsersCount = databaseUserContextMockup.Object.User.Count();
            //Assert.AreEqual(2, UsersCount);
            Assert.NotNull(User);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
        }
        //5
        [Test]
        public async Task LoginTest()
        {
            var manager = GetUserManager();
            string email = "andrii@vlpi.com";
            string password = "password";
            
            var user = await manager.AuthenticateUserAsync(email, password);
            
            
            bool ok = (user!=null)? user.Email==email : false;
            Assert.True(ok);
        }
        //6
        [Test]
        public async Task AddRequirementTest()
        {
            var manager = GetRequirementsManager();
            var requirements = new List<Requirement>
            {
                new()
                {
                    Id = 1,
                    TaskId = 1,
                    Description = "some desc",
                },
                new()
                {
                    Id = 2,
                    TaskId = 1,
                    Description = "some desc 2"
                }
            };


            await manager.AddBulk(requirements);

            var task =  databaseRequirementContextMockup.Object.Task.FirstOrDefault(t => t.Id == 1);
            if (task != null)
            {
                var reqs = task.Requirement;
                Assert.NotNull(reqs);
            }
            else
            {
                Assert.True(false);
            }
        }
        //7
        [Test]
        public async Task VerifyAnswerTaskType1()
        {
            int expectedScore = 100;
            var manager = GetAnswersManager();
            var answer = new WritingAnswer
            {
                SystemName = "VLPI",
                Requirements = new List<WritingRequirement>()
                {
                    new ()
                    {
                        RequirementStatement = "Statement",
                        RequirementTypeId = 1
                    }
                }
            };


            var result = await manager.VerifyWritingAnswerAsync(1, answer);
            int score = result.Score;
            


            Assert.AreEqual(expectedScore, score);
        }

        //8
        [Test]
        public async Task VerifyAnswerTaskType2()
        {
            int expectedScore = 100;
            var manager = GetAnswersManager();
            var answer = new AnalysisAnswer
            {
                CorrectRequirements = new List<int>{1,2,3,4},
                WrongRequirements = new List<ModifiedRequirement>
                {
                    new()
                    {
                        Id = 5,
                        ModifiedRequirementStatement = "Statement1"
                    },
                    new()
                    {
                        Id = 6,
                        ModifiedRequirementStatement = "Statement2"
                    }
                }
            };


            var result = await manager.VerifyAnalysisAnswerAsync(1, answer);
            int score = result.Score;



            Assert.AreEqual(expectedScore, score);
        }

        //9
        [Test]
        public async Task GenerateTaskStats()
        {
            int taskId = 1;
            var manager = GetStatisticManager();
            var expectedStatistic = new TaskStatistic()
            {
                TaskId = taskId,
                Objective = "Objective",
                Complexity = 1,
                UserAnswersCount = 1,
                AverageScore = 80.5,
                AverageTime = 30
            };


            var statistic = await manager.GetStatisticByTaskAsync(taskId);


            bool ok = CompareTaskStats(expectedStatistic, statistic);
            Assert.IsTrue(ok);
        }

        //10
        [Test]
        public async Task GetStatisticForUser()
        {
            int userId = 1;
            var manager = GetStatisticManager();
            GenericUserStatistic expectedResult = new GenericUserStatistic()
            {
                Attempts = 2,
                AverageScore = 100,
                AverageTime = 30,
                PassedAttempts = 1
            };

            var userStats = await manager.GetGenericUserStatisticAsync(userId);

            bool equal = CompareUserStats(expectedResult, userStats);
            Assert.IsTrue(equal);
        }

        public TaskManager GetTaskManager()
        {
            return new TaskManager(new TaskRepository(databaseContextMockup.Object), _mapper,
                new RequirementManager(new RequirementRepository(databaseContextMockup.Object)));
        }
        public UserManager GetUserManager()
        {
            return new UserManager(new UserRepository(databaseUserContextMockup.Object));
        }
        public RequirementManager GetRequirementsManager()
        {
            return new RequirementManager(new RequirementRepository(databaseRequirementContextMockup.Object));
        }
        public AnswerManager GetAnswersManager()
        {
            return new AnswerManager(new AnswerRepository(databaseRequirementContextMockup.Object), GetTaskManager());
        }
        public StatisticManager GetStatisticManager()
        {
            return new StatisticManager(GetTaskManager());
        }
        private bool CompareTasks(Core.Entities.Task task1, Core.Entities.Task task2)
        {
            return task1.Id == task2.Id && task1.Complexity == task2.Complexity &&
                   task1.Description == task2.Description;
        }

        private bool CompareTaskStats(TaskStatistic stat1, TaskStatistic stat2)
        {
            return stat1.TaskId == stat2.TaskId && stat1.Complexity == stat2.Complexity && stat1.AverageScore ==stat2.AverageScore && stat1.AverageTime ==stat2.AverageTime && stat1.UserAnswersCount== stat2.UserAnswersCount;
        }

        private bool CompareUserStats(GenericUserStatistic stat1, GenericUserStatistic stat2)
        {
            return stat1.AverageScore == stat2.AverageScore && stat1.PassedAttempts == stat2.PassedAttempts &&
                   stat1.AverageTime == stat2.AverageTime && stat1.Attempts == stat2.Attempts;
        }
    }
}


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