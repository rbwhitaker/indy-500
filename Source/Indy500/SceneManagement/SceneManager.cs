using System.Collections.Generic;

namespace Indy500.SceneManagement
{
    public class SceneManager
    {
        private Dictionary<SceneState, IScene> scenes = new Dictionary<SceneState, IScene>();
        public IScene ActiveScene => scenes[currentState];

        private SceneState currentState;

        public void TransitionTo(SceneState sceneState)
        {
            currentState = sceneState;
            ActiveScene.Update(new Microsoft.Xna.Framework.GameTime());
        }

        public void AddScene(SceneState state, IScene scene)
        {
            scenes[state] = scene;
        }
    }
}