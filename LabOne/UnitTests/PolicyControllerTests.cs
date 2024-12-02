using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using LabOne.Controllers;
using LabOne.Services;
using System.Web.Http;
using System.Web.Http.Results;
using System.Collections.Generic;
using System.Linq;
using System;

namespace LabOne.Tests.Controllers
{
    /// <summary>
    /// Unit tests for the PolicyController class.
    /// </summary>
    [TestClass]
    public class PolicyControllerTests
    {
        private Mock<IPolicyService> _mockPolicyService;
        private PolicyController _controller;

        /// <summary>
        /// Initializes the test setup.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _mockPolicyService = new Mock<IPolicyService>();
            _controller = new PolicyController(_mockPolicyService.Object);
        }

        /// <summary>
        /// Tests if GetPolicyById returns Ok when the policy exists.
        /// </summary>
        [TestMethod]
        public void GetPolicyById_PolicyExists_ReturnsOk()
        {
            // Arrange
            var policy = new Policy { PolicyId = 1 };
            _mockPolicyService.Setup(service => service.GetPolicyById(1)).Returns(policy);

            // Act
            IHttpActionResult actionResult = _controller.GetPolicyById(1);
            var contentResult = actionResult as OkNegotiatedContentResult<Policy>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.PolicyId);
        }

        /// <summary>
        /// Tests if GetPolicyById returns NotFound when the policy does not exist.
        /// </summary>
        [TestMethod]
        public void GetPolicyById_PolicyDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            _mockPolicyService.Setup(service => service.GetPolicyById(1)).Returns((Policy)null);

            // Act
            IHttpActionResult actionResult = _controller.GetPolicyById(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        /// <summary>
        /// Tests if GetAllPolicies returns all policies.
        /// </summary>
        [TestMethod]
        public void GetAllPolicies_ReturnsAllPolicies()
        {
            // Arrange
            var policies = new List<Policy> { new Policy { PolicyId = 1 }, new Policy { PolicyId = 2 } };
            _mockPolicyService.Setup(service => service.GetAllPolicies()).Returns(policies);

            // Act
            IHttpActionResult actionResult = _controller.GetAllPolicies();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Policy>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(2, contentResult.Content.Count());
        }

        /// <summary>
        /// Tests if AddPolicy returns Created when the policy is valid.
        /// </summary>
        [TestMethod]
        public void AddPolicy_ValidPolicy_ReturnsCreated()
        {
            // Arrange
            var policy = new Policy { PolicyId = 1, Email = "test@example.com", AccountNumber = "12345678", PolicyNumber = "12345678", FirstName = "John", LastName = "Doe", PostalCode = "1234567", Phone = "123-456-7890", ProducerCode = "12345", GroupCode = "12345", MasterCode = "12345", City = "City", State = "State", EffectiveDate = DateTime.Now, ExpirationDate = DateTime.Now.AddYears(1), AnnualPremium = 100.00M };

            // Act
            IHttpActionResult actionResult = _controller.AddPolicy(policy);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Policy>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.AreEqual(1, createdResult.RouteValues["id"]);
        }

        /// <summary>
        /// Tests if AddPolicy returns BadRequest when the policy is invalid.
        /// </summary>
        [TestMethod]
        public void AddPolicy_InvalidPolicy_ReturnsBadRequest()
        {
            // Arrange
            var policy = new Policy();
            _controller.ModelState.AddModelError("Email", "Email is required");

            // Act
            IHttpActionResult actionResult = _controller.AddPolicy(policy);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(InvalidModelStateResult));
        }

        /// <summary>
        /// Tests if UpdatePolicy returns NoContent when the policy is valid.
        /// </summary>
        [TestMethod]
        public void UpdatePolicy_ValidPolicy_ReturnsNoContent()
        {
            // Arrange
            var policy = new Policy { PolicyId = 1, Email = "test@example.com", AccountNumber = "12345678", PolicyNumber = "12345678", FirstName = "John", LastName = "Doe", PostalCode = "1234567", Phone = "123-456-7890", ProducerCode = "12345", GroupCode = "12345", MasterCode = "12345", City = "City", State = "State", EffectiveDate = DateTime.Now, ExpirationDate = DateTime.Now.AddYears(1), AnnualPremium = 100.00M };

            // Act
            IHttpActionResult actionResult = _controller.UpdatePolicy(1, policy);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(StatusCodeResult));
            var statusCodeResult = actionResult as StatusCodeResult;
            Assert.AreEqual(System.Net.HttpStatusCode.NoContent, statusCodeResult.StatusCode);
        }

        /// <summary>
        /// Tests if UpdatePolicy returns BadRequest when the policy is invalid.
        /// </summary>
        [TestMethod]
        public void UpdatePolicy_InvalidPolicy_ReturnsBadRequest()
        {
            // Arrange
            var policy = new Policy { PolicyId = 1 };
            _controller.ModelState.AddModelError("Email", "Email is required");

            // Act
            IHttpActionResult actionResult = _controller.UpdatePolicy(1, policy);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(InvalidModelStateResult));
        }

        /// <summary>
        /// Tests if DeletePolicy returns Ok when the policy exists.
        /// </summary>
        [TestMethod]
        public void DeletePolicy_PolicyExists_ReturnsOk()
        {
            // Arrange
            var policy = new Policy { PolicyId = 1 };
            _mockPolicyService.Setup(service => service.GetPolicyById(1)).Returns(policy);

            // Act
            IHttpActionResult actionResult = _controller.DeletePolicy(1);
            var contentResult = actionResult as OkNegotiatedContentResult<Policy>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.PolicyId);
        }

        /// <summary>
        /// Tests if GetUserName returns Ok when the policy exists.
        /// </summary>
        [TestMethod]
        public void GetUserName_PolicyExists_ReturnsOk()
        {
            // Arrange
            var policy = new Policy { PolicyId = 1, FirstName = "John", LastName = "Doe" };
            _mockPolicyService.Setup(service => service.GetPolicyById(1)).Returns(policy);

            // Act
            IHttpActionResult actionResult = _controller.GetUserName(1);
            var contentResult = actionResult as OkNegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("John Doe", contentResult.Content);
        }

        /// <summary>
        /// Tests if GetUserName returns NotFound when the policy does not exist.
        /// </summary>
        [TestMethod]
        public void GetUserName_PolicyDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            _mockPolicyService.Setup(service => service.GetPolicyById(1)).Returns((Policy)null);

            // Act
            IHttpActionResult actionResult = _controller.GetUserName(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        /// <summary>
        /// Tests if GetMiddleName returns Ok when the policy exists.
        /// </summary>
        [TestMethod]
        public void GetMiddleName_PolicyExists_ReturnsOk()
        {
            // Arrange
            _mockPolicyService.Setup(service => service.GetMiddleName(1)).Returns("MiddleName");

            // Act
            IHttpActionResult actionResult = _controller.GetMiddleName(1);
            var contentResult = actionResult as OkNegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("MiddleName", contentResult.Content);
        }

        /// <summary>
        /// Tests if GetMiddleName returns NotFound when the policy does not exist.
        /// </summary>
        [TestMethod]
        public void GetMiddleName_PolicyDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            _mockPolicyService.Setup(service => service.GetMiddleName(1)).Throws(new KeyNotFoundException());

            // Act
            IHttpActionResult actionResult = _controller.GetMiddleName(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        /// <summary>
        /// Tests if GetListOfEmailIds returns Ok.
        /// </summary>
        [TestMethod]
        public void GetListOfEmailIds_ReturnsOk()
        {
            // Arrange
            var emailIds = new List<string> { "test1@example.com", "test2@example.com" };
            _mockPolicyService.Setup(service => service.listOfEmailIds()).Returns(emailIds);

            // Act
            IHttpActionResult actionResult = _controller.GetListOfEmailIds();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<string>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(2, contentResult.Content.Count());
        }

        /// <summary>
        /// Tests if GetListOfEmailIds returns InternalServerError when an exception occurs.
        /// </summary>
        [TestMethod]
        public void GetListOfEmailIds_InternalServerError()
        {
            // Arrange
            _mockPolicyService.Setup(service => service.listOfEmailIds()).Throws(new Exception());

            // Act
            IHttpActionResult actionResult = _controller.GetListOfEmailIds();

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ExceptionResult));
        }
    }
}
