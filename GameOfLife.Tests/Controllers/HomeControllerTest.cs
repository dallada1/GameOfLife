using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLife.Controllers;
using GameOfLife.Models;
using System.Collections.Generic;
using GameOfLifeDomain;

namespace GameOfLife.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private HomeController controller;
        private String threeBlockHorizontalOscillatorSeededAtOneOne = "1,1&1,2&1,3";

        public HomeControllerTest()
        {
            controller = new HomeController();
        }

        [TestMethod]
        public void TestIndexWithTwentyByTwentyGrid()
        {
            var result = controller.Index() as ViewResult;
            
            var model = result.Model as GameOfLifeModel;
            Assert.IsNotNull(result);
            Assert.AreEqual(400, model.CellsModel.Cells.Count());
        }

        [TestMethod]
        public void SeedGridWithOneOneAndOneTwoAndOneThree()
        {
            var result = controller.SeedGrid(threeBlockHorizontalOscillatorSeededAtOneOne) as ViewResult;

            var model = result.Model as GameOfLifeModel;
            Assert.IsNotNull(result);
            Assert.AreEqual(400, model.CellsModel.Cells.Count());
            Assert.IsTrue(model.CellsModel.Cells.First(c => c.XCoordinate == 1 && c.YCoordinate == 1).IsAlive);
            Assert.IsTrue(model.CellsModel.Cells.First(c => c.XCoordinate == 1 && c.YCoordinate == 2).IsAlive);
            Assert.IsTrue(model.CellsModel.Cells.First(c => c.XCoordinate == 1 && c.YCoordinate == 3).IsAlive);
            Assert.AreEqual(3, model.CellsModel.Cells.Count(c => c.IsAlive));
        }

        [TestMethod]
        public void SeedGridWithEmptyString()
        {
            var result = controller.SeedGrid(String.Empty) as ViewResult;

            var model = result.Model as GameOfLifeModel;

            Assert.IsNotNull(result);
            Assert.AreEqual(400, model.CellsModel.Cells.Count());
            Assert.AreEqual(0, model.CellsModel.Cells.Count(c => c.IsAlive));
        }

        [TestMethod]
        public void SeedGridWithOneOneAndOneTwoAndOneThreeAndIncrementGeneration()
        {
            var result = controller.SeedGrid(threeBlockHorizontalOscillatorSeededAtOneOne) as ViewResult;
            var modelBeforeIncrementGeneration = result.Model as GameOfLifeModel;
            result = controller.IncrementGeneration(modelBeforeIncrementGeneration.CellsModel.Cells.ToList(), false) as ViewResult;

            var modelAfterIncrementGeneration = result.Model as GameOfLifeModel;

            Assert.IsTrue(modelAfterIncrementGeneration.CellsModel.Cells.First(c => c.XCoordinate == 0 && c.YCoordinate == 2).IsAlive);
            Assert.IsTrue(modelAfterIncrementGeneration.CellsModel.Cells.First(c => c.XCoordinate == 1 && c.YCoordinate == 2).IsAlive);
            Assert.IsTrue(modelAfterIncrementGeneration.CellsModel.Cells.First(c => c.XCoordinate == 2 && c.YCoordinate == 2).IsAlive);
        }

        [TestMethod]
        public void SeedGridFromTemplateWithBoxAtOneOne()
        {
            var result = controller.SeedGridFromTemplate("1,1", "Box") as ViewResult;
            var modelBeforeIncrementGeneration = result.Model as GameOfLifeModel;

            var modelAfterIncrementGeneration = result.Model as GameOfLifeModel;

            Assert.IsTrue(modelAfterIncrementGeneration.CellsModel.Cells.First(c => c.XCoordinate == 1 && c.YCoordinate == 1).IsAlive);
            Assert.IsTrue(modelAfterIncrementGeneration.CellsModel.Cells.First(c => c.XCoordinate == 1 && c.YCoordinate == 2).IsAlive);
            Assert.IsTrue(modelAfterIncrementGeneration.CellsModel.Cells.First(c => c.XCoordinate == 2 && c.YCoordinate == 1).IsAlive);
            Assert.IsTrue(modelAfterIncrementGeneration.CellsModel.Cells.First(c => c.XCoordinate == 2 && c.YCoordinate == 2).IsAlive);
        }

        [TestMethod]
        public void SeedGridFromTemplateWithVerticalLineAtOneOne()
        {
            var result = controller.SeedGridFromTemplate("1,1", "VerticalLine") as ViewResult;
            var modelBeforeIncrementGeneration = result.Model as GameOfLifeModel;

            Assert.IsTrue(modelBeforeIncrementGeneration.CellsModel.Cells.First(c => c.XCoordinate == 1 && c.YCoordinate == 1).IsAlive);
            Assert.IsTrue(modelBeforeIncrementGeneration.CellsModel.Cells.First(c => c.XCoordinate == 2 && c.YCoordinate == 1).IsAlive);
            Assert.IsTrue(modelBeforeIncrementGeneration.CellsModel.Cells.First(c => c.XCoordinate == 3 && c.YCoordinate == 1).IsAlive);
        }

        [TestMethod]
        public void SeedGridFromTemplateWithHorizontalLineAtOneOne()
        {
            var result = controller.SeedGridFromTemplate("1,1", "HorizontalLine") as ViewResult;
            var modelBeforeIncrementGeneration = result.Model as GameOfLifeModel;

            Assert.IsTrue(modelBeforeIncrementGeneration.CellsModel.Cells.First(c => c.XCoordinate == 1 && c.YCoordinate == 1).IsAlive);
            Assert.IsTrue(modelBeforeIncrementGeneration.CellsModel.Cells.First(c => c.XCoordinate == 1 && c.YCoordinate == 2).IsAlive);
            Assert.IsTrue(modelBeforeIncrementGeneration.CellsModel.Cells.First(c => c.XCoordinate == 1 && c.YCoordinate == 3).IsAlive);
        }

        [TestMethod]
        public void SeedGridFromTemplateWithToadOneOne()
        {
            var result = controller.SeedGridFromTemplate("1,1", "Toad") as ViewResult;
            var modelBeforeIncrementGeneration = result.Model as GameOfLifeModel;

            Assert.IsTrue(modelBeforeIncrementGeneration.CellsModel.Cells.First(c => c.XCoordinate == 1 && c.YCoordinate == 1).IsAlive);
            Assert.IsTrue(modelBeforeIncrementGeneration.CellsModel.Cells.First(c => c.XCoordinate == 1 && c.YCoordinate == 2).IsAlive);
            Assert.IsTrue(modelBeforeIncrementGeneration.CellsModel.Cells.First(c => c.XCoordinate == 1 && c.YCoordinate == 3).IsAlive);
            Assert.IsTrue(modelBeforeIncrementGeneration.CellsModel.Cells.First(c => c.XCoordinate == 2 && c.YCoordinate == 2).IsAlive);
            Assert.IsTrue(modelBeforeIncrementGeneration.CellsModel.Cells.First(c => c.XCoordinate == 2 && c.YCoordinate == 3).IsAlive);
            Assert.IsTrue(modelBeforeIncrementGeneration.CellsModel.Cells.First(c => c.XCoordinate == 2 && c.YCoordinate == 4).IsAlive);
        }

        [TestMethod]
        public void SeedGridFromTemplateWithGliderOneOne()
        {
            var result = controller.SeedGridFromTemplate("1,1", "Glider") as ViewResult;
            var modelBeforeIncrementGeneration = result.Model as GameOfLifeModel;

            Assert.IsTrue(modelBeforeIncrementGeneration.CellsModel.Cells.First(c => c.XCoordinate == 1 && c.YCoordinate == 1).IsAlive);
            Assert.IsTrue(modelBeforeIncrementGeneration.CellsModel.Cells.First(c => c.XCoordinate == 1 && c.YCoordinate == 3).IsAlive);
            Assert.IsTrue(modelBeforeIncrementGeneration.CellsModel.Cells.First(c => c.XCoordinate == 2 && c.YCoordinate == 2).IsAlive);
            Assert.IsTrue(modelBeforeIncrementGeneration.CellsModel.Cells.First(c => c.XCoordinate == 3 && c.YCoordinate == 2).IsAlive);
            Assert.IsTrue(modelBeforeIncrementGeneration.CellsModel.Cells.First(c => c.XCoordinate == 2 && c.YCoordinate == 3).IsAlive);
            Assert.IsTrue(modelBeforeIncrementGeneration.CellsModel.Cells.First(c => c.XCoordinate == 3 && c.YCoordinate == 2).IsAlive);
        }

        [TestMethod]
        public void PressPlayThenPauseButton()
        {
            var result = controller.Play(new List<Cell> {new Cell(1,1)}) as ViewResult;
            var modelBeforeIncrementGeneration = result.Model as GameOfLifeModel;

            Assert.IsTrue(modelBeforeIncrementGeneration.AutoRun);

            result = controller.Pause(new List<Cell> { new Cell(1, 1) }) as ViewResult;
            modelBeforeIncrementGeneration = result.Model as GameOfLifeModel;

            Assert.IsFalse(modelBeforeIncrementGeneration.AutoRun);
        }
    }

}
