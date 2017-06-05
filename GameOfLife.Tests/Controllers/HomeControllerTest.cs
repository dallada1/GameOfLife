using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLife;
using GameOfLife.Controllers;
using GameOfLife.Models;

namespace GameOfLife.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            var cellsModel = result.Model as CellsModel;
            Assert.IsNotNull(result);
            Assert.AreEqual(100, cellsModel.Cells.Count());
        }
    }
}
