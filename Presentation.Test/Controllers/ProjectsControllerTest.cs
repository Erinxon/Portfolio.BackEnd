using Application.Services.Proyects.Commands;
using Application.Services.ProyectSkills.Commands;
using FluentAssertions;
using MediatR;
using Moq;
using Presentation.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Controllers
{
    public class ProjectsControllerTest
    {
        private readonly IMediator _mediator;

        public ProjectsControllerTest()
        {
            this._mediator = new Mock<IMediator>().Object;
        }

        [Fact]
        public async void GetProjectsOK()
        {
            /// Arrange
            var projectsController = new ProjectsController(_mediator);
            var UserId = 3;
            /// Act
            var result = await projectsController.Get(UserId);
            /// Assert
            result?.Value?.Succeeded.Should().Be(true);
        }

        [Fact]
        public async void GetProjectsHasValue()
        {
            /// Arrange
            var projectsController = new ProjectsController(_mediator);
            var UserId = 3;
            /// Act
            var result = await projectsController.Get(UserId);
            /// Assert
            result?.Value?.Data.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async void CreateProjectSuccced()
        {
            /// Arrange
            var projectsController = new ProjectsController(_mediator);
            CreateProjectCommand createProjectCommand = new CreateProjectCommand()
            {
                Name = "Aplicación para registrar gastos e ingresos",
                Description = "Aplicación para registrar gastos e ingresos - Proyecto Final - Curso Desarrollo Web Front-End Con Angular - ITLA",
                GithubUrl = "https://github.com/Erinxon/app-angular-gastos-ingresos",
                DomainUrl = "https://gestor-economia.vercel.app/home",
                PlatformId = 1,
                UserId = 3,
                CreateProyectSkillCommands = new List<CreateProjectSkillCommand>()
                {
                    new CreateProjectSkillCommand(0,0,10)
                }
            };
            /// Act
            var result = await projectsController.Post(createProjectCommand);
            /// Assert
            result?.Value?.Succeeded.Should().Be(true);
        }

        [Fact]
        public async void CreateProjectFailed()
        {
            /// Arrange
            var projectsController = new ProjectsController(_mediator);
            CreateProjectCommand createProjectCommand = new CreateProjectCommand();
            /// Act
            var result = await projectsController.Post(createProjectCommand);
            /// Assert
            result?.Value?.Succeeded.Should().Be(false);
        }
        
    }
}
