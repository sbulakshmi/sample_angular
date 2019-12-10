using System;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using asp_dotnet_core_angular.Controllers;
using asp_dotnet_core_angular.Persistence;
using AutoMapper;
using asp_dotnet_core_angular.Model;
using System.Collections.Generic;
using asp_dotnet_core_angular.Controllers.Resources;

namespace asp_dotnet_core_angular
{
    public class UnitTest1
    {
        #region snippet_VehiclesControllerTests1
        [Fact]
        public async Task Create_ReturnsBadRequest_GivenInvalidModel()
        {

            // Arrange & Act
            var mockRepo = new Mock<IVehicleRepository>();
            var mockMapper = new Mock<IMapper>();
            var mockUOW = new Mock<IUnitOfWork>();

            var controller = new VehiclesController(mockMapper.Object, mockRepo.Object, mockUOW.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await controller.CreateVehicle(vehicleResource: null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region snippet_VehiclesControllerTests2
        // [Fact]
        // public async Task ForVehicle_ReturnsHttpNotFound_ForInvalidVehicle()
        // {
        //     // Arrange
        //     int testVehicleId = 123;
        //     var mockRepo = new Mock<IVehicleRepository>();
        //     var mockMapper = new Mock<IMapper>();
        //     var mockUOW = new Mock<IUnitOfWork>();

        //     // mockRepo.Setup(repo => repo.GetVehicle(testVehicleId))
        //     //     .ReturnsAsync((Vehicle)null);
        //     var controller = new VehiclesController(mockMapper.Object, mockRepo.Object, mockUOW.Object);


        //     // Act
        //     var result = await controller.GetVehicle(testVehicleId);

        //     // Assert
        //     Assert.IsType<NotFoundResult>(result);
        // }
        // #endregion

        // #region snippet_VehiclesControllerTests3
        // [Fact]
        // public async Task Create_ReturnsNewlyCreatedVehicle()
        // {
        //     // Arrange
        //     int testModelId = 2;
        //     bool testIsRegistered = true;
        //     string testContactName = "ab";
        //     string testContactPhone = "23";
        //     string testContactEmail = "sd";
        //     List<int>  testFeature= new List<int>{1,2,3};

        //     var mockRepo = new Mock<IVehicleRepository>();
        //     var mockMapper = new Mock<IMapper>();
        //     var mockUOW = new Mock<IUnitOfWork>();


            
        //     var controller = new VehiclesController(mockMapper.Object, mockRepo.Object, mockUOW.Object);


        //     var newVehicle = new SaveVehicleResource()
        //     {
        //       ModelId =testModelId,
        //       IsRegistered= testIsRegistered,
        //       Contact = new ContactResource()
        //       {
        //           Name = testContactName,
        //           Phone = testContactPhone,
        //           Email = testContactEmail
        //       },
        //       Features = testFeature
  
        //     };
        //     mockRepo.Setup(repo => repo.AddVehicle(new Vehicle()))
        //     .Verifiable();

        //     mockUOW.Setup (uow => uow.CompleteAsync())
        //     .Returns(Task.CompletedTask)
        //     .Verifiable();

        //     // Act
        //     var result = await controller.CreateVehicle(newVehicle);

        //     // Assert
        //     var okResult = Assert.IsType<OkObjectResult>(result);
            
        // }

        private object GetTestVehicle()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
