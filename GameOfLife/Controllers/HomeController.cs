using GameOfLife.Models;
using GameOfLifeDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameOfLife.Controllers
{
    public class HomeController : Controller
    {
        private IRule rule;
        private GameController gameController;
        private IEnumerable<Cell> cells;
        //private CellsModel model;

        public HomeController()
        {
            this.rule = new DefaultRuleSet();
            //var emptySeed = new List<Cell>();
            //this.gameController = new GameController(emptySeed, rule);
            //this.cells = gameController.GetCells();
            //model = new CellsModel();
            //model.Cells = cells;
        }

        public ActionResult Index()
        {
            var emptySeed = new List<Cell>();
            var gameController = new GameController(emptySeed, rule);
            var cells = gameController.GetCells();
            var model = new CellsModel()
            {
                Cells = cells
            };

            return View(model);
        }

        public ActionResult Initialize()
        {
            var seededCell1 = new Cell(1, 2);
            var seededCell2 = new Cell(2, 1);
            var seededCell3 = new Cell(2, 2);
            var seededCell4 = new Cell(3, 2);
            var seededCell5 = new Cell(1, 3);
            var liveCellSeed = new List<Cell>()
            {
                seededCell1,
                seededCell2,
                seededCell3,
                seededCell4,
                seededCell5
            };
            gameController = new GameController(liveCellSeed, rule);
            cells = gameController.GetCells();
            var model = new CellsModel()
            {
                Cells = cells
            };

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Submit(List<Cell> cells)
        {
            var liveCellSeed = cells.Where(c => c.IsAlive).ToList();
            gameController = new GameController(liveCellSeed, rule);
            var newCells = gameController.GetNextGeneration();
            var model = new CellsModel()
            {
                Cells = newCells
            };

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult NextGeneration(IEnumerable<Cell> incomingCells)
        {
            var liveCellSeed = incomingCells.Where(c => c.IsAlive).ToList();
            gameController = new GameController(liveCellSeed, rule);
            cells = gameController.GetNextGeneration();
            var model = new CellsModel()
            {
                Cells = cells
            };

            return View("Index", model);
        }
    }
}