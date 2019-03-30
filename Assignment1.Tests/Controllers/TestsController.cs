using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment1.Models;
using System.Collections.Generic;
using Moq;
using System.Linq;
using Assignment1.Controllers;
using System.Web.Mvc;


namespace Assignment1.Tests.Controllers
{
    [TestClass]
    public class TestsControllerTest
    {
        // moq data
        TestsController controller;
        List<Test> tests;
        Mock<IMockTests> mock;

        [TestInitialize]
        public void TestInitialize()
        {
            // create some mock data
            tests = new List<Test>
            {
                new Test { Id = 500, name = "Fake Test One", author = "Tester" },
                new Test { Id = 501, name = "Fake Test Two" , author = "tester"},
                new Test { Id = 503, name = "Fake Test Three", author = "Toster" }
            };

            // set up & populate our mock object to inject into our controller
            mock = new Mock<IMockTests>();
            mock.Setup(c => c.Tests).Returns(tests.AsQueryable());


            // initialize the controller and inject the mock object
            controller = new TestsController(mock.Object);
        }

        [TestMethod]
        public void IndexViewLoads()
        {
            // arrange
            // now handled in TestInitialize

            // act
            ViewResult result = controller.Index() as ViewResult;

            // assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void IndexLoadsTests()
        {
            // act
            // call the index method
            // access the data model returned to the view
            // cast the data as a list of type Category
            var results = (List<Test>)((ViewResult)controller.Index()).Model;

            // assert
            CollectionAssert.AreEqual(tests.OrderBy(c => c.Id).ToList(), results);
        }
        [TestMethod]
        public void DetailsIdNull() {
            var result = (ViewResult)controller.Details(null);

            Assert.AreEqual("Error", result.ViewName);
                
        }

        [TestMethod]
        public void DetailsIdMissing()
        {
            var result = (ViewResult)controller.Details(-1);

            Assert.AreEqual("Error", result.ViewName);

        }


        [TestMethod]
        public void DetailsIdValid()
        {
            Test result = (Test)((ViewResult)controller.Details(500)).Model;
            Assert.AreEqual(tests[0], result);
        }

        [TestMethod]
        public void DetailsCorrectView()
        {
            ViewResult result = (ViewResult)controller.Details(500);
            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]
        public void EditValidTestView()
        {
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Edit(tests[0]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }


        [TestMethod]
        public void EditIdNull()
        {
            int? id = null;

            var result = (ViewResult)controller.Edit(id);

            Assert.AreEqual("Error", result.ViewName);

        }

        [TestMethod]
        public void EditIdInvalid()
        {
            var result = (ViewResult)controller.Edit(-1);

            Assert.AreEqual("Error", result.ViewName);

        }

        [TestMethod]
        public void EditIdValid()
        {
            Test result = (Test)((ViewResult)controller.Edit(tests[0].Id)).Model;
            Assert.AreEqual(tests[0], result);
        }


        [TestMethod]
        public void CreateViewValid()
        {
            ViewResult result = (ViewResult)controller.Create();
            Assert.AreEqual("Create", result.ViewName);
        }


        [TestMethod]
        public void DeleteIdNull()
        {
            int? id = null;

            var result = (ViewResult)controller.Delete(id);

            Assert.AreEqual("Error", result.ViewName);

        }

        [TestMethod]
        public void DeleteIdInvalid()
        {
            var result = (ViewResult)controller.Delete(-1);

            Assert.AreEqual("Error", result.ViewName);

        }

        [TestMethod]
        public void DeleteIdValid()
        {
            Test result = (Test)((ViewResult)controller.Delete(tests[0].Id)).Model;
            Assert.AreEqual(tests[0], result);
        }

        [TestMethod]
        public void DeleteValidTestView()
        {
            ViewResult result = (ViewResult)controller.Delete(tests[0].Id);
            Assert.AreEqual("Delete", result.ViewName);
        }

        [TestMethod]
        public void EditPostInvalidState()
        {
            Test invalid = new Test { Id = tests[0].Id };
            controller.ModelState.AddModelError("Edit Error", "the test is invalid");
            Test result = (Test)((ViewResult)controller.Edit(invalid)).Model;
            Assert.AreEqual(result, invalid);
        }

        [TestMethod]
        public void EditPostInvalidStateView()
        {
            Test invalid = new Test { Id = tests[0].Id };
            controller.ModelState.AddModelError("Edit Error", "the test is invalid");
            ViewResult result = (ViewResult)controller.Edit(invalid);
            Assert.AreEqual("Edit", result.ViewName);
        }

        [TestMethod]
        public void CreateValidTest()
        {
            Test valid = new Test { Id = 1999, name="valid", author ="valid",description="valid" };
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Create(valid);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void CreateInValidTestView()
        {
            Test invalid = new Test { Id = -1, name = "invalid", author = "invalid", description = "invalid" };
            controller.ModelState.AddModelError("Create Error", "the test is invalid");
            ViewResult result = (ViewResult)controller.Create(invalid);
            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void CreateInValidTestValue()
        {
            Test invalid = new Test { Id = -1, name = "invalid", author = "invalid", description = "invalid" };
            controller.ModelState.AddModelError("Create Error", "the test is invalid");
            Test result = (Test)((ViewResult)controller.Create(invalid)).Model;
            Assert.AreEqual(invalid, result);
        }

        [TestMethod]
        public void DeleteConfirmedNoId()
        {
            int? id = null;
            ViewResult result = (ViewResult)controller.DeleteConfirmed(id);
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmedMissingId()
        {
            ViewResult result = (ViewResult)controller.DeleteConfirmed(-1);
            Assert.AreEqual("Error", result.ViewName);
        }


        [TestMethod]
        public void DeleteConfirmedCorrectIdView()
        {
            RedirectToRouteResult result = (RedirectToRouteResult)controller.DeleteConfirmed(tests[0].Id);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}
