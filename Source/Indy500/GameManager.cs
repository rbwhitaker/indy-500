using Indy500.SceneManagement;

namespace Indy500
{
    public class GameManager
    {
        public MessageDispatcher MessageDispatcher { get; }

        public Race CurrentRace { get; private set; }

        public GameManager(MessageDispatcher messageDispatcher)
        {
            MessageDispatcher = messageDispatcher;
            MakeNewLevel();
        }

        public void MakeNewLevel()
        {
            Level level = Level.Parse(System.IO.File.ReadAllText("LevelExample.txt"));
            CurrentRace = RaceBuilder.FromLevel(level, MessageDispatcher);
        }
    }
}
