using GameOfLife.Models;
using GameOfLifeDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GameOfLife.Controllers
{
    public class HomeController : Controller
    {
        private IRule rule;
        private GameController gameController;
        private List<Cell> cells;

        public HomeController()
        {
            this.rule = new DefaultRuleSet();
        }

        public ActionResult Submit(string submitButton, List<Cell> cells, Boolean runAutomatically)
        {
            switch (submitButton)
            {
                case "Play":
                    return (Play(cells));
                case "Pause":
                    return (Pause(cells));
                case "Next Generation":
                    return (IncrementGeneration(cells, runAutomatically));
                default:
                    return (View());
            }
        }

        public ActionResult Index()
        {
            var emptySeed = new List<Cell>();
            var gameController = new GameController(emptySeed, rule);
            var cells = gameController.GetCells();
            var model = new GameOfLifeModel()
            {
                CellsModel = new CellsModel() { Cells = cells },
                TextModel = new TextModel()
            };

            return View(model);
        }

        public ActionResult SeedGrid(String textInput)
        {
            var listOfCells = new List<Cell>();
            if (!String.IsNullOrEmpty(textInput))
            {
                var delimitedInput = textInput.Split(new char[] { '&' });
                foreach (var pair in delimitedInput)
                {
                    var individualPairs = pair.Split(new char[] { ',' });
                    var tempCell = new Cell(Convert.ToInt32(individualPairs[0]), Convert.ToInt32(individualPairs[1]));
                    listOfCells.Add(tempCell);
                }
            }
            gameController = new GameController(listOfCells, rule);
            cells = gameController.GetCells();
            var model = CreateGameOfLifeModel(cells, false);

            return View("Index", model);
        }

        private GameOfLifeModel CreateGameOfLifeModel(List<Cell> newCells, Boolean autoRun)
        {
            return new GameOfLifeModel()
            {
                CellsModel = new CellsModel() { Cells = newCells },
                TextModel = new TextModel(),
                AutoRun = autoRun
            };
        }

        [HttpPost]
        public ActionResult IncrementGeneration(List<Cell> cells, Boolean runAutomatically)
        {
            if (runAutomatically)
                System.Threading.Thread.Sleep(500);
            var liveCellSeed = cells.Where(c => c.IsAlive).ToList();
            gameController = new GameController(liveCellSeed, rule);
            var newCells = gameController.GetNextGeneration().ToList();
            var model = CreateGameOfLifeModel(newCells, runAutomatically);

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult SeedGridFromTemplate(String textInput, String RadioButton)
        {
            Random rand = new Random();
            var XCoord = 9;
            var YCoord = 9;
            var listOfCells = new List<Cell>();
            if (!String.IsNullOrEmpty(textInput))
            {
                var delimitedInput = textInput.Split(new char[] { ',' });
                XCoord = Convert.ToInt32(delimitedInput[0]);
                YCoord = Convert.ToInt32(delimitedInput[1]);
            }

            switch (RadioButton)
            {
                case "Box":
                    listOfCells.Add(new Cell(XCoord, YCoord));
                    listOfCells.Add(new Cell(XCoord + 1, YCoord));
                    listOfCells.Add(new Cell(XCoord, YCoord + 1));
                    listOfCells.Add(new Cell(XCoord + 1, YCoord + 1));
                    break;
                case "VerticalLine":
                    listOfCells.Add(new Cell(XCoord, YCoord));
                    listOfCells.Add(new Cell(XCoord + 1, YCoord));
                    listOfCells.Add(new Cell(XCoord + 2, YCoord));
                    break;
                case "HorizontalLine":
                    listOfCells.Add(new Cell(XCoord, YCoord));
                    listOfCells.Add(new Cell(XCoord, YCoord + 1));
                    listOfCells.Add(new Cell(XCoord, YCoord + 2));
                    break;
                case "Toad":
                    listOfCells.Add(new Cell(XCoord, YCoord));
                    listOfCells.Add(new Cell(XCoord, YCoord + 1));
                    listOfCells.Add(new Cell(XCoord, YCoord + 2));
                    listOfCells.Add(new Cell(XCoord + 1, YCoord + 1));
                    listOfCells.Add(new Cell(XCoord + 1, YCoord + 2));
                    listOfCells.Add(new Cell(XCoord + 1, YCoord + 3));
                    break;
                case "Glider":
                    listOfCells.Add(new Cell(XCoord, YCoord));
                    listOfCells.Add(new Cell(XCoord, YCoord + 2));
                    listOfCells.Add(new Cell(XCoord + 1, YCoord + 1));
                    listOfCells.Add(new Cell(XCoord + 2, YCoord + 1));
                    listOfCells.Add(new Cell(XCoord + 1, YCoord + 2));
                    listOfCells.Add(new Cell(XCoord + 2, YCoord + 1));
                    break;
            }
            
            listOfCells = CheckBounds(listOfCells);
            gameController = new GameController(listOfCells, rule);
            cells = gameController.GetCells();
            if (RadioButton == "Random")
                
                for (int i = 0; i < 400; i++)
                {
                    if(rand.Next(2) == 0)
                        cells.ElementAt(i).IsAlive = true;
                }
            var model = CreateGameOfLifeModel(cells, false);

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Play(List<Cell> cells)
        {
            var newModel = CreateGameOfLifeModel(cells, false);
            newModel.AutoRun = true;

            return View("Index", newModel);
        }

        [HttpPost]
        public ActionResult Pause(List<Cell> cells)
        {
            var newModel = CreateGameOfLifeModel(cells, false);
            newModel.AutoRun = false;

            return View("Index", newModel);
        }

        private List<Cell> CheckBounds(List<Cell> cells)
        {
            var newList = new List<Cell>();
            foreach (var cell in cells)
            {
                cell.IsAlive = true;
                if (cell.XCoordinate < 20 && cell.XCoordinate > -1 && cell.YCoordinate < 20 && cell.YCoordinate > -1)
                    newList.Add(cell);
            }
            return newList;
        }
    }
}