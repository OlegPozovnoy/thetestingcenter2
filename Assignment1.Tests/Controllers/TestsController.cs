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
            var result = (ViewResult)controller.Details(null);

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
    }
}
