using GameOfLife.Models;

namespace GameOfLife.Models
{
    public class GameOfLifeModel
    {
        public CellsModel CellsModel { get; set; }
        public TextModel TextModel { get; set; }

        public GameOfLifeModel()
        {
            this.CellsModel = new CellsModel();
            this.TextModel = new TextModel();
        }
    }
}